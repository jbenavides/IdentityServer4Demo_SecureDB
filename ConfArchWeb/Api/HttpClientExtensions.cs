using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace ConfArchWeb.Api
{
    public static class HttpClientExtensions
    {
        public static async void SetBearerToken(this HttpClient client, HttpContext context)
        {
            var accessToken =
                await context.Authentication.GetTokenAsync("access_token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
