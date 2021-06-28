using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AccountRole
    {
        public AccountRole()
        {
        }

        public AccountRole(Account account, Role role)
        {
            this.account = account;
            this.role = role;
        }

        [Key]
        public int AccountRoleId { get; set; }
        public virtual Account account { get; set; }
        public string AccountId { get; set; }
        public virtual Role role { get; set; }
        public int RoleId { get; set; }

    }
}
