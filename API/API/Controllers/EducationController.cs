using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : BaseController<Education, EducationRepository, int>
    {
        private readonly EducationRepository educationRepository;
        public EducationController(EducationRepository repository) : base(repository)
        {
            educationRepository = repository;
        }
        [HttpGet("/API/Education/GetUniEducation/{id}")]
        public ActionResult GetUniEducation(int id)
        {
            var get = educationRepository.GetUniEducation(id);
            
            return Ok(get);
    }
    }

   
}
