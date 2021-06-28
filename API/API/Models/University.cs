﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_University")]
    public class University
    {
        public int Id { get; set; }
        public string UniversityName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Education> educations { get; set; }
    }
}
