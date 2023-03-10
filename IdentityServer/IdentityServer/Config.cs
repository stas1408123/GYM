using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ("GYM.API", "GYM API")
               // new("NewsAggregator", "News aggregator")
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
                    AllowedScopes = { "GYM.API" }
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
                new ApiResource("GYM.API")
            };
    }


}
