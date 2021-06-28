using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_T_Account")]
    public class Account
    {
        public Account()
        {
        }

        public Account(string nIK, string password, Employee employee)
        {
            NIK = nIK;
            Password = password;
            this.employee = employee;
        }

        [Key]
        public string NIK { get; set; }
        public string Password { get; set; }
        public string Guid { get; set; }
        [JsonIgnore]
        public virtual Profiling profiling { get; set; }
        [JsonIgnore]
        public virtual Employee employee { get; set; }
        public virtual ICollection<AccountRole> accountRoles { get; set; }
    }
}
