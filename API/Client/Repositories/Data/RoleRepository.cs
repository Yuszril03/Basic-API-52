using API.Models;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class RoleRepository : GeneralRepository<Role, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public RoleRepository(Address address, string request = "Role/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<Role>> GetRoleView()
        {
            List<Role> entities = new List<Role>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Role>>(apiResponse);
            }
            return entities;
        }
        public async Task<List<Role>> GetRoleView(int roleId)
        {
            //List<Education> entities = new List<Education>();
            List<Role> entities = new List<Role>();
            using (var response = await httpClient.GetAsync(request + "/GetUniEducation/" + roleId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Role>>(apiResponse);
            }
            return entities;
        }
    }
}
