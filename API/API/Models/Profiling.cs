using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_T_Profiling")]
    public class Profiling
    {
        public Profiling()
        {
        }

        public Profiling(string nIK, Account account, Education education)
        {
            NIK = nIK;
            this.account = account;
            this.education = education;
        }

        [Key]
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual Account account  { get; set; }
        //[JsonIgnore]
        public virtual Education education { get; set; }
        public  int educationId { get; set; }

    }
}
