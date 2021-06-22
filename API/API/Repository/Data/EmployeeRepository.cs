using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository :GeneralRepository<MyContext,Employee,string>
    {
        private readonly MyContext myContext1;
        public EmployeeRepository(MyContext myContext): base(myContext)
        {
            this.myContext1 = myContext;
        }
        public int Register(RegisterVM registerVM)
        {
            try
            {
                int hasil = 1;
                var cekNIK = myContext1.Employees.SingleOrDefault(e => e.NIK == registerVM.NIK);
                var cekEmail = myContext1.Employees.SingleOrDefault(e=> e.Email==registerVM.Email);
                if (cekNIK == null && cekEmail==null)
                {
                    Employee e = new Employee(registerVM.NIK, registerVM.FirstName, registerVM.LastName, registerVM.Email, registerVM.Salary, registerVM.PhoneNumber, (Models.Gender)registerVM.Gender, registerVM.BirthDate);
                    Account account = new Account(registerVM.NIK, registerVM.Password, e);

                    var x = myContext1.University.SingleOrDefault(e => e.Id == registerVM.UniversityId);
                    Education education = new Education(registerVM.Degree, registerVM.GPA, x);
                    Profiling profiling = new Profiling(registerVM.NIK, account, education);


                    var em = myContext1.Employees.Add(e);
                    var ac = myContext1.Account.Add(account);
                    var pro = myContext1.Profiling.Add(profiling);
                    var edu = myContext1.Education.Add(education);

                    var xd = myContext1.SaveChanges();
                    hasil = 1;
                }
                else if (cekNIK != null && cekEmail == null)
                {
                    hasil = 2; // nik sudah ada

                }else if (cekNIK == null && cekEmail != null)
                {
                    hasil = 3; //email sudah ada
                }
                else if (cekNIK != null && cekEmail != null)
                {
                    hasil = 4; //NIK & email sudah ada
                }
                return hasil;
               
            }
            catch (DbUpdateException)
            {

                return 0; // gagal daftar
            }
        }
        public int Login(LoginVM loginVM)
        {
            int hasil = 0 ;
            if (loginVM.NIK != "" && loginVM.Password!="")
            {
                var cekEmployeeEmail = myContext1.Employees.SingleOrDefault(e => e.Email == loginVM.NIK);
                var cekEmployeeNIK = myContext1.Employees.SingleOrDefault(e => e.NIK == loginVM.NIK);
                if(cekEmployeeEmail != null)
                {
                    var cekPassword = myContext1.Account.SingleOrDefault(e => e.NIK == cekEmployeeEmail.NIK && e.Password == loginVM.Password);
                    if (cekPassword != null)
                    {
                        hasil = 1; // benar
                    }
                    else
                    {
                        hasil = 5; // Jika pw salah
                    }
                }
                else if (cekEmployeeNIK != null)
                {
                    var cekPassword = myContext1.Account.SingleOrDefault(e => e.NIK == loginVM.NIK && e.Password == loginVM.Password);
                    if (cekPassword != null)
                    {
                        return 1; // Benar
                    }
                    else
                    {
                        return 5; // Jika PW Salah
                    }
                }
                else
                {
                    hasil = 4; // Jika nik/email null/tdak ada
                }
            }
            else if (loginVM.NIK == "" && loginVM.Password != "")
            {
                hasil=2; // Jika nik/email null
            }
            else if (loginVM.NIK != "" && loginVM.Password == "")
            {
                hasil = 3; ///Jika password null
            }
            else
            {
                hasil = 0; // kosong semua
            }

            

            return hasil;
           
         
        }

    }
}
