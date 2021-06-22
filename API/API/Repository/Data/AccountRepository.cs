using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        public readonly MyContext myContext1;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext1 = myContext;
        }

        public int Login(LoginVM loginVM)
        {
            var getData = myContext1.Account.FirstOrDefault(e => e.NIK == loginVM.NIK && e.Password == loginVM.Password);
            if (getData != null) return 1;
            return 0;

        }
    }
}
