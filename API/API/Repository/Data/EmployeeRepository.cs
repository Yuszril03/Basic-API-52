using API.Context;
using API.Hashing;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository :GeneralRepository<MyContext,Employee,string>
    {
        private readonly MyContext myContext1;
        Hash hash = new Hash();
        public EmployeeRepository(MyContext myContext): base(myContext)
        {
            this.myContext1 = myContext;
        }
        public string ResetPassword(ResetPassword resetPassword)
        {
            var hasil = "";
            var getEmail = myContext1.Employees.SingleOrDefault(e=> e.Email==resetPassword.Email);
            if (getEmail != null)
            {
                var getPassword = myContext1.Account.Find(getEmail.NIK);
                Guid obj = Guid.NewGuid();
                getPassword.Guid = obj.ToString();

                myContext1.Entry(getPassword).State = EntityState.Modified;
                myContext1.SaveChanges();
                hasil = obj.ToString()+"."+getEmail.FirstName;

            }
            else
            {
                hasil = "";
            }
            return hasil;
        }

        public int ChangePassword(ChangePassword changePassword)
        {
           
            if(changePassword.NIK!=null && changePassword.PasswordOld!=null  && changePassword.PasswordNew!=null)
            {
                var cekNIK = myContext1.Employees.SingleOrDefault(e => e.NIK == changePassword.NIK);
                var cekEmail = myContext1.Employees.SingleOrDefault(e => e.Email == changePassword.NIK);
                if (cekNIK != null)
                {
                    var getData = myContext1.Account.SingleOrDefault(e => e.NIK == changePassword.NIK && e.Guid == changePassword.PasswordOld);
                    if (getData != null)
                    {
                        getData.Guid = null;
                        getData.Password = hash.HashingPassword(changePassword.PasswordNew);
                        myContext1.Entry(getData).State = EntityState.Modified;
                        myContext1.SaveChanges();
                        return 1; // Benar
                    }
                    return 6;

                }else if (cekEmail != null)
                {
                    var getData = myContext1.Account.SingleOrDefault(e => e.NIK == changePassword.NIK && e.Guid == changePassword.PasswordOld);
                    if (getData != null)
                    {
                        getData.Guid = null;
                        getData.Password = hash.HashingPassword(changePassword.PasswordNew);
                        myContext1.Entry(getData).State = EntityState.Modified;
                        myContext1.SaveChanges();
                        return 1; // Benar
                    }
                    return 6;
                }

                return 5; // NIK / EMail Salah
            }
            
            else if (changePassword.NIK == null && changePassword.PasswordOld != null && changePassword.PasswordNew != null)
            {
                return 2; // NIK KOSONG
            }
           
            else if (changePassword.NIK != null && changePassword.PasswordOld == null && changePassword.PasswordNew != null)
            {
                return 3; // PASSOLD KOSONG
            }
            
            else if (changePassword.NIK != null && changePassword.PasswordOld != null && changePassword.PasswordNew == null)
            {
                return 4; // PassNew KOSONG
            }
           
           
            
            return 0;
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
                    Account account = new Account(registerVM.NIK,hash.HashingPassword(registerVM.Password), e);
                    var role = myContext1.Role.Find(2);

                    var x = myContext1.University.SingleOrDefault(e => e.Id == registerVM.UniversityId);
                    Education education = new Education(registerVM.Degree, registerVM.GPA, x);
                    Profiling profiling = new Profiling(registerVM.NIK, account, education);
                    AccountRole accountRole = new AccountRole(account,role);


                    var em = myContext1.Employees.Add(e);
                    var ac = myContext1.Account.Add(account);
                    var pro = myContext1.Profiling.Add(profiling);
                    var acrol = myContext1.AccountRole.Add(accountRole);
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
                    var cekPassword = myContext1.Account.SingleOrDefault(e => e.NIK == cekEmployeeEmail.NIK);
                    if (hash.ValidatePassword(loginVM.Password, cekPassword.Password) == true)
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
                    var cekPassword = myContext1.Account.SingleOrDefault(e => e.NIK == loginVM.NIK);
                    if (hash.ValidatePassword(loginVM.Password, cekPassword.Password) == true )
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
        public List<string> GetDataLogin(LoginVM loginVM)
        {
            List<string> data = new List<string>();
            var cekEmployeeEmail = myContext1.Employees.SingleOrDefault(e => e.Email == loginVM.NIK);
            var cekEmployeeNIK = myContext1.Employees.SingleOrDefault(e => e.NIK == loginVM.NIK);
            if (cekEmployeeEmail != null)
            {
                data.Add(cekEmployeeEmail.Email);
                var cekAccount = myContext1.AccountRole.SingleOrDefault(e => e.AccountId == cekEmployeeEmail.NIK);
                var cekRole = myContext1.Role.Find(cekAccount.RoleId);
                data.Add(cekRole.RoleName);
               
            }
            else if (cekEmployeeNIK != null)
            {
                data.Add(cekEmployeeNIK.Email);
                var cekAccount = myContext1.AccountRole.SingleOrDefault(e => e.AccountId == cekEmployeeNIK.NIK);
                var cekRole = myContext1.Role.Find(cekAccount.RoleId);
                data.Add(cekRole.RoleName);
            }
            return data;
        }
        public IQueryable GetProfil(string nik)
        {
            var getNIK = myContext1.Employees.Find(nik);
            var getEmail = myContext1.Employees.SingleOrDefault(e=>e.Email==nik);
            if (getEmail != null)
            {
                var q = (from em in myContext1.Employees
                         join ac in myContext1.Account on em.NIK equals ac.NIK
                         join pro in myContext1.Profiling on ac.NIK equals pro.NIK
                         join edu in myContext1.Education on pro.educationId equals edu.Id
                         join uni in myContext1.University on edu.universityId equals uni.Id
                         where em.NIK == getEmail.NIK
                         select new
                         {
                             em.NIK,
                             em.FirstName,
                             em.LastName,
                             em.Email,
                             em.Salary,
                             em.PhoneNumber,
                             em.BirthDate,
                             em.Gender,
                             ac.Password,
                             edu.Degree,
                             edu.GPA,
                             uni.UniversityName
                         });
                return q;
            }
            else if (getNIK != null)
            {
                var q = (from em in myContext1.Employees
                         join ac in myContext1.Account on em.NIK equals ac.NIK
                         join ar in myContext1.AccountRole on ac.NIK equals ar.AccountId
                         join r in myContext1.Role on ar.RoleId equals r.RoleId
                         join pro in myContext1.Profiling on ac.NIK equals pro.NIK
                         join edu in myContext1.Education on pro.educationId equals edu.Id
                         join uni in myContext1.University on edu.universityId equals uni.Id
                         where em.NIK == nik
                         select new
                         {
                             em.NIK,
                             em.FirstName,
                             em.LastName,
                             em.Email,
                             em.Salary,
                             em.PhoneNumber,
                             em.BirthDate,
                             em.Gender,
                             r.RoleName,
                             //ac.Password,
                             edu.Degree,
                             edu.GPA,
                             uni.UniversityName
                         });
                return q;
            }
            return null;
           
        }
        public IEnumerable GetProfil()
        {
            var q = (from em in myContext1.Employees
                     join ac in myContext1.Account on em.NIK equals ac.NIK
                     join ar in myContext1.AccountRole on ac.NIK equals ar.AccountId
                     join r in myContext1.Role on ar.RoleId equals r.RoleId
                     join pro in myContext1.Profiling on ac.NIK equals pro.NIK
                     join edu in myContext1.Education on pro.educationId equals edu.Id
                     join uni in myContext1.University on edu.universityId equals uni.Id
                     select new
                     {
                         em.NIK,
                         em.FirstName,
                         em.LastName,
                         em.Email,
                         em.Salary,
                         em.PhoneNumber,
                         em.BirthDate,
                         em.Gender,
                         r.RoleName,
                         //ac.Password,
                         edu.Degree,
                         edu.GPA,
                         uni.UniversityName
                     });
            return q.ToList();
        }
    }
}
