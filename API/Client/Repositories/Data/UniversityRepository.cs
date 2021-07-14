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
    public class UniversityRepository : GeneralRepository<University, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public UniversityRepository(Address address, string request = "University/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<University>> GetUniversityView()
        {
            List<University> entities = new List<University>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<University>>(apiResponse);
            }
            return entities;
        }
        public async Task<List<University>> GetUniversityView(int universityId)
        {
            List<University> entities = new List<University>();

            using (var response = await httpClient.GetAsync(request + "/1"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<University>>(apiResponse);
            }
            return entities;
        }
    }
}
