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
    public class AccountRoleController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository accountRoleRepository;
        public AccountRoleController(AccountRoleRepository repository) : base(repository)
        {
            this.accountRoleRepository = repository;
        }
        //[HttpGet("/API/AccountRole/DeleteData/{nik}")]
        //public ActionResult DeleteData(string nik)
        //{
        //    var get = accountRoleRepository.DeleteNik(nik);
        //    if (get != 0)
        //    {
        //        return Ok(get);
        //    }
        //    return BadRequest();
        //}
    }
}
