using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
       
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [HttpPost("/API/Employees/Register")]
        public ActionResult Register(RegisterVM re)
        {
            int  registrasi = employeeRepository.Register(re);
            switch (registrasi)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, result = registrasi, message = "Registrasi Berhasil" });
                    break;
                case 2:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = registrasi, message = "NIK sudah terdaftar" });
                    break; 
                case 3:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = registrasi, message = "Email sudah terdaftar" });
                    break; 
                case 4:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = registrasi, message = "NIK dan Email sudah terdaftar" });
                    break;
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = registrasi, message = "Registrasi Gagal" });
                    break;
            }
        }
        [HttpPost("/API/Employees/Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            int login = employeeRepository.Login(loginVM);
            switch (login)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, result = login, message = "Login Sukses" });
                    break;
                case 2:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "NIK atau Email Kosong" });
                    break;
                case 3:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "Password Kosong" });
                    break;
                case 4:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "NIK atau Email Belum Terdaftar Database" });
                    break;
                case 5:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "Password Salah" });
                    break;
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "NIK dan Password Kosong" });
                    break;


            }
        }
       
    }
}
