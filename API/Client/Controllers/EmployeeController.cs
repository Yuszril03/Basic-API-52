using API.Models;
using Client.Base;
using Client.Repositories.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Client.Controllers
{
    public class EmployeeController :  BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Employee/GetRegistrasiView")]
        public async Task<JsonResult> GetRegistrasiView()
        {
            var result = await repository.GetRegistrasiView();
            return Json(result);
        }
        [HttpGet("Employee/GetRegistrasiView/{nik}")]
        public async Task<JsonResult> GetRegistrasiView(string nik)
        {
            var result = await repository.GetRegistrasiView(nik);
            return Json(result);
        }
        
        [HttpPost("Employee/LoginData")]
        public async Task<IActionResult> LoginData(LoginVM loginVM)
        {
            var jwtToken = await repository.LoginData(loginVM);
            if(jwtToken == null)
            {
                return RedirectToAction("Landing", "Template");
            }
            HttpContext.Session.SetString("JWToken", jwtToken.Token);
            HttpContext.Session.SetString("Name", repository.JwtName(jwtToken.Token));
            return RedirectToAction("Index", "Template");
        }

        [HttpGet("Employee/CekLogin/{nik}/{pw}")]
        public async Task<JsonResult> CekLogin(string nik, string pw)
        {
            LoginVM loginVM = new LoginVM();
            loginVM.NIK = nik;
            loginVM.Password = pw;
            var jwtToken = await repository.LoginData(loginVM);
            return Json(jwtToken);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("JWToken", "");
            HttpContext.Session.SetString("Name", "");
            return RedirectToAction("Landing", "Template");
        }
    }
}
