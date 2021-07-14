using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext myContext1;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext1 = myContext;
        }

        //public int DeleteNik(string nik)
        //{
        //    int jml = 0;
        //    var find = (from ar in myContext1.AccountRole
        //                where ar.AccountId == nik
        //                select new {
        //                    ar.AccountRoleId
        //                }
        //                );
        //    foreach (var item in find)
        //    {
        //        myContext1.AccountRole.Find(item.AccountRoleId);
        //    }
        //    return 0;
        //}
    }
}
