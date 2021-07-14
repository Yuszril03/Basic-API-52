using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class TemplateController : Controller
    {
       
        public IActionResult Index()
        {   
            return View();
        }
        [AllowAnonymous]
        public IActionResult Landing()
        {
            return View("Landing");
        }
        public IActionResult Employee()
        {
            return View("Employee");
        }
        public IActionResult Role()
        {
            return View("Role");
        }
        public IActionResult Education()
        {
            return View("Education");
        }
     
        public IActionResult University()
        {
            return View("University");
        }
        public IActionResult ResetPassword()
        {
            return View("ResetPassword");
        }
    }
}
