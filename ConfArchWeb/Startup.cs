using System;
using System.Net.Http;
using ConfArchWeb.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConfArchWeb
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ConferenceApiService>();
            services.AddTransient<ProposalApiService>();
            services.AddTransient<AttendeeApiService>();
            services.AddSingleton(x => new HttpClient {BaseAddress = new Uri("http://localhost:54438")});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, HttpClient httpClient)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();
            
            // cookie middleware first
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            //openId connect middleware
            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies", //where to store the identity information, this is reference to the cookie middleware

                Authority = "http://localhost:5000", //URL base of the token service
                RequireHttpsMetadata = false, //overrides the default requirement that only Https traffic from the token service is allowed. (just for dev)

                ClientId = "confarchweb",
                ClientSecret = "secret",

                ResponseType = "code id_token", //determines which grant is being used. in this case is a combination of authorization code and identity token.
                Scope = { "confArchApi" },// a list of scopes we want this client to request.
                SaveTokens = true // indicates we want to store the access token in a cookie.
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
