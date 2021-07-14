using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class EducationVM
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public int universityId { get; set; }
    }
}
