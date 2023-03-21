using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ("GYM.API", "GYM API"),
                new("News.Api", "News API"),
                new ("SwaggerAPI", "Swagger API"),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
               new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientId/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    // scopes that client has access to
                    AllowedScopes =
                    {
                        "GYM.API",
                        "SwaggerAPI",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }

                },
                new Client
                {
                    ClientId = "client_id",

                    // no interactive user, use the clientId/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    // scopes that client has access to
                    AllowedScopes = { "News.API" }
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                   
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:7213/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:7213/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                new Client
                {
                    ClientId = "swagger_id",
                    ClientSecrets = { new Secret("secret".ToSha256()) },
                    AllowedGrantTypes =  GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedCorsOrigins = { "https://localhost:7163","https://localhost:7181" },
                    AllowedScopes =
                    {
                        "SwaggerAPI",
                        "GYM.API",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                new Client
                {
                    ClientId = "swagger_id",
                    ClientSecrets = { new Secret("secret".ToSha256()) },
                    AllowedGrantTypes =  GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedCorsOrigins = { "https://localhost:7163","https://localhost:7181" },
                    AllowedScopes =
                    {
                        "SwaggerAPI",
                        "GYM.API",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };

        public static IEnumerable<IdentityResource> GetResources =>
            new List<IdentityResource>
            {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> GetApiResources =>
            new List<ApiResource>
            {
                new ("News.Api", "News API"),
                new ("SwaggerAPI", "Swagger API")
            };
    }
}
