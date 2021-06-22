using API.Base;
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
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
       private readonly AccountRepository accountRepository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }
        [HttpPost("/API/Account/Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var login = accountRepository.Login(loginVM);
            if (login > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = login, message = "Login Sukses" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "Login Gagal" });
            }
        }
    }
}
