using System;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.OpenApi.Writers;

namespace ShoppingApp.Api.API
{
    public class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("app.api.shopping.full", "Full Access for Shopping API"),
                new ApiScope("app.api.shopping.read", "Read ONLY Access for Shopping API")
            };

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource {
                    Name = "app.api.shopping",
                    DisplayName = "Shopping Api",
                    Scopes = new List<string>()
                    {
                        "app.api.shopping.read",
                        "app.api.shopping.full"
                    }
                },
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
// resource owner password grant client
                new Client
                {
                    ClientName = "Client Application1",
                    ClientId = "t8agr5xKt4$3",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = { new Secret("eb300de4-add9-42f4-a3ac-abd3c60f1919".Sha256()) },
                    AllowedScopes = new List<string> { "app.api.shopping.full", "app.api.shopping.read" }
                }
            };
        }
    }
}