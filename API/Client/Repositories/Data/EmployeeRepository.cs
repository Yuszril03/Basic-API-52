using API.Models;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using API.ViewModel;
using System.IdentityModel.Tokens.Jwt;

namespace Client.Repositories.Data
{
    public class EmployeeRepository: GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //JWT
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }



        public async Task<List<RegisterVM>> GetRegistrasiView()
        {
            List<RegisterVM> entities = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request + "GetProfil/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return entities;
        }
        public async Task<List<RegisterVM>> GetRegistrasiView(string nik)
        {
            List<RegisterVM> entities = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request + "GetProfil/"+nik))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<JwtTokenVM> LoginData(LoginVM loginVM)
        {
            JwtTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "Login/", content);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<JwtTokenVM>(apiResponse);
            }
            return token;
        }
        public string JwtName(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken result = tokenHandler.ReadJwtToken(token);

            return result.Claims.FirstOrDefault(claim => claim.Type.Equals("Name")).Value;
        }

    }
}
