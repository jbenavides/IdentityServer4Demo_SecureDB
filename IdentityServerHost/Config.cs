// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerHost
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("employeeInfo", new [] {"employeeno"})
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("confArchApi", "ConfArch API", new []{"employeeno"})
                //if we set some claims, these will be included in the access token
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ExternalApiClient",
                    

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"confArchApi"}, // will have access just to the API.
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                },

                new Client
                {
                    ClientId = "confarchweb",
                    ClientName = "ConfArch MVC Client",

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },                    

                    RedirectUris = {"http://localhost:51705/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:51705"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "confArchApi"
                        //this have access to the two identity scopes plus the API.
                    },
                    AllowedGrantTypes = GrantTypes.Hybrid
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "d10841fe-d702-434b-9050-745eea366b87",
                    Username = "jbenavides",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Jose"),
                        new Claim("website", "http://jbenavides.com")
                    }
                }
            };
        }
    }
}