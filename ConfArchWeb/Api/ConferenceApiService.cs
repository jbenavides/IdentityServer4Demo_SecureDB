using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConfArchShared.Models;
using Microsoft.AspNetCore.Http;

namespace ConfArchWeb.Api
{
    public class ConferenceApiService
    {
        private readonly HttpClient client;

        public ConferenceApiService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            client.SetBearerToken(httpContextAccessor.HttpContext);
        }
        public async Task<IEnumerable<ConferenceModel>> GetAll()
        {
            List<ConferenceModel> result;
            var response = await client.GetAsync("/Conference/GetAll");
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsAsync<List<ConferenceModel>>();
            else
                throw new HttpRequestException(response.ReasonPhrase);

            return result;
        }

        public async Task<ConferenceModel> GetById(int id)
        {
            var result = new ConferenceModel();
            var response = await client.GetAsync($"/Conference/GetById/{id}");
            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadAsAsync<ConferenceModel>();
 
            return result;
        }

        public async Task Add(ConferenceModel model)
        {
            await client.PostAsJsonAsync("/Conference/Add/", model);
        }
    }
}
