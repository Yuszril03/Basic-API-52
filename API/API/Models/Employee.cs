﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Employee")]
    public class Employee
    {
       

        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        //[JsonIgnore]
        public virtual Account account { get; set; }

    }
}
