using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers.Data
{
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
