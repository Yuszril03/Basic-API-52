using API.Context;
using API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EducationRepository : GeneralRepository<MyContext, Education, int>
    {
        private readonly MyContext myContext1;
        public EducationRepository(MyContext myContext) : base(myContext)
        {
            myContext1 = myContext;
        }
        public IQueryable GetUniEducation(int id)
        {
            var q = (from edu in myContext1.Education
                     where edu.Id ==id
                     select new { edu.Id, edu
            .Degree,edu.GPA,edu.universityId});

            return q;
        }
    }
}
