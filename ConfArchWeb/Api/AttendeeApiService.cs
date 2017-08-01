using System.Net.Http;
using System.Threading.Tasks;
using ConfArchShared.Models;
using Microsoft.AspNetCore.Http;

namespace ConfArchWeb.Api
{
    public class AttendeeApiService
    {
        private readonly HttpClient client;

        public AttendeeApiService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            client.SetBearerToken(httpContextAccessor.HttpContext);
        }

        public async Task<AttendeeModel> GetById(int attendeeId)
        {
            AttendeeModel result = null;
            var response = await client.GetAsync($"/Attendee/GetById/{attendeeId}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<AttendeeModel>();
            }
            return result;
        }

        public async Task<AttendeeModel> Add(AttendeeModel attendee)
        {
            AttendeeModel result = null;
            var response = await client.PostAsJsonAsync("/Attendee/Add", attendee);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<AttendeeModel>();
            }
            return result;
        }

        public async Task<int> GetAttendeesTotal(int conferenceId)
        {
            var  result = 0;
            var response = await client.GetAsync($"/Attendee/GetAttendeesTotal/{conferenceId}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<int>();
            }
            return result;
        }
    }
}
