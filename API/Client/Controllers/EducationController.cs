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
    public class EducationController : BaseController<Education, EducationRepository, int>
    {
        private readonly EducationRepository repository;
        public EducationController(EducationRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Education/GetEducationView")]
        public async Task<JsonResult> GetEducationView()
        {
            var result = await repository.GetEducationView();
            return Json(result);
        }
        
    }
}
