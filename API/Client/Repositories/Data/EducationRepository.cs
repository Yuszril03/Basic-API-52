using API.Models;
using Client.Base;
using Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EducationRepository : GeneralRepository<Education, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public EducationRepository(Address address, string request = "Education/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<Education>> GetEducationView()
        {
            List<Education> entities = new List<Education>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Education>>(apiResponse);
            }
            return entities;
        }
        public async Task<List<EducationVM>> GetEducationView(int educationId)
        {
            //List<Education> entities = new List<Education>();
            List<EducationVM> entities = new List<EducationVM>();
            using (var response = await httpClient.GetAsync(request+ "/GetUniEducation/" + educationId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<EducationVM>>(apiResponse);
            }
            return entities;
        }
    }
}
