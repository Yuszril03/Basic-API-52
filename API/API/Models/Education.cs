using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Education")]
    public class Education
    {
        public Education()
        {
        }

        public Education(string degree, string gPA, University university)
        {
            Degree = degree;
            GPA = gPA;
            this.university = university;
        }

        public int Id { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        [JsonIgnore]
        public virtual University university { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profiling> profilings { get; set; }
    }
}
