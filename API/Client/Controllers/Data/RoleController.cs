using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class RoleController : BaseController<Role, RoleRepository, int>
    {
        private readonly RoleRepository repository;
        public RoleController(RoleRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Role/GetRoleView")]
        public async Task<JsonResult> GetRoleView()
        {
            var result = await repository.GetRoleView();
            return Json(result);
        }
        [HttpGet("University/GetRoleView/{id}")]
        public async Task<JsonResult> GetRoleView(int universityId)
        {
            var result = await repository.GetRoleView(universityId);
            return Json(result);
        }
    }
}
