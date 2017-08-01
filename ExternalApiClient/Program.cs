using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalApiClient
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();


        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ExternalApiClient", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("confArchApi");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            GetTotalAttendeePosted(client).GetAwaiter().GetResult();


            var response = await client.PostAsync("http://localhost:54438/Attendee/Post/1/Roland", null);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                GetTotalAttendeePosted(client).GetAwaiter().GetResult();
                Console.WriteLine("Attendee posted");
            }
            Console.ReadKey();
        }

        private static async Task GetTotalAttendeePosted(HttpClient client)
        {
            var resp = await client.GetAsync("http://localhost:54438/Attendee/GetAttendeesTotal/1");
            if (resp.IsSuccessStatusCode)
            {
                using (HttpContent content = resp.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    Console.WriteLine($"Number of attendees are: {result.Result}");
                }
            }
        }
    }
}