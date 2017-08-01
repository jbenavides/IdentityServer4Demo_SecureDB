using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConfArchShared.Models;
using Microsoft.AspNetCore.Http;

namespace ConfArchWeb.Api
{  
    public class ProposalApiService
    {
        private readonly HttpClient client;

        public ProposalApiService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            client.SetBearerToken(httpContextAccessor.HttpContext);
        }

        public async Task<IEnumerable<ProposalModel>> GetAllForConference(int conferenceId)
        {
            var result = new List<ProposalModel>();
            var response = await client.GetAsync($"/Proposal/GetAll/{conferenceId}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<List<ProposalModel>>();
            }
            return result;
        }

        public async Task<IEnumerable<ProposalModel>> GetAllApprovedForConference(int conferenceId)
        {
            var result = new List<ProposalModel>();
            var response = await client.GetAsync($"/Proposal/GetAllApprovedForConference/{conferenceId}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<List<ProposalModel>>();
            }
            return result;
        }

        public async Task Add(ProposalModel model)
        {
            var response = await client.PostAsJsonAsync("/Proposal/Add/", model);
        }

        public async Task<ProposalModel> Approve(int proposalId)
        {
            var result = new ProposalModel();
            var response = await client.GetAsync($"/Proposal/Approve/{proposalId}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<ProposalModel>();
            }
            return result;
        }
    }
}
