using API.Base;
using API.Context;
using API.JWT;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private IConfiguration _config;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration _config) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._config = _config;
        }

        [HttpPost("/API/Employees/Register")]
        public ActionResult Register(RegisterVM re)
        {
            int  registrasi = employeeRepository.Register(re);
            switch (registrasi)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, result = registrasi, message = "Registrasi Berhasil" });
                    break;
                case 2:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = registrasi, message = "NIK sudah terdaftar" });
                    break; 
                case 3:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = registrasi, message = "Email sudah terdaftar" });
                    break; 
                case 4:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = registrasi, message = "NIK dan Email sudah terdaftar" });
                    break;
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = registrasi, message = "Registrasi Gagal" });
                    break;
            }
        }
        [HttpPost("/API/Employees/Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            
            int login = employeeRepository.Login(loginVM);
            switch (login)
            {
                case 1:
                    GenerateJWT jwt = new GenerateJWT(_config);
                    var data = employeeRepository.GetDataLogin(loginVM);
                    var isi = jwt.GetJWT(data[0],data[1]);
                    return Ok(new { status = HttpStatusCode.OK, result = isi, message = "Login Sukses" });
                case 2:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "NIK atau Email Kosong" });
                    break;
                case 3:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "Password Kosong" });
                    break;
                case 4:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "NIK atau Email Belum Terdaftar Database" });
                    break;
                case 5:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "Password Salah" });
                    break;
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = login, message = "NIK dan Password Kosong" });
                    break;


            }
        }

        [HttpPost("/API/Employees/ResetPassword")]
        public ActionResult ResetPassword(ResetPassword resetPassword)
        {
            var  getData = employeeRepository.ResetPassword(resetPassword);
            if (getData != "")
            {
                string[] split = getData.Split(".");
                var fromAddress = new MailAddress("aplikasiapi52@gmail.com", "Aplikasi API");
                var toAddress = new MailAddress(resetPassword.Email, split[1]);
                const string fromPassword = "mcc52api";
                 string subject = "Reset Password "+ DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                string body = "Hai, "+split[1]+" berikut password kamu yang sekarang : "+split[0]+ ". Segera lakukan Change Password.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Email Telah Terkirim" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email tidak terdaftar dalam database" });
            }
           
        }
        [HttpPost("/API/Employees/ChangePassword")]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            var get = employeeRepository.ChangePassword(changePassword);
            switch (get)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, result = get, message = "Password Berhasil Diperbarui" });
                    break;
                case 2:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "NIK harap diisi" });
                    break;
                case 3:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "Password Lama harap diisi" });
                    break;
                case 4:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "Password Baru harap diisi" });
                    break;
                case 5:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "NIK atau Email Tidak terdaftar didatabase" });
                    break;
                case 6:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "Password lama salah" });
                    break;
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = get, message = "Form harap diisi semua" });
                    break;


            }
           
        }


        [Authorize]
        [HttpGet("/API/Employees/GetProfil/{nik}")]
        public ActionResult GetProfil(string nik)
        {

            var getData = employeeRepository.GetProfil(nik);
            if (getData != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getData, message = "Data Berhasil Tampil" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = getData, message = "Nik atau Email Tidak tersimpan di database" });
            }
        }
        [Authorize]
        [HttpGet("/API/Employees/GetProfil")]
        public ActionResult GetProfil()
        {
            var getData = employeeRepository.GetProfil();
            if (getData != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getData, message = "Data Berhasil Tampil" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = getData, message = "Data masih kosong" });
            }


        }
    }
}
