using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ("GYM.API", "GYM API")
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
                }
            };

        public static IEnumerable<IdentityResource> GetResources =>
            new List<IdentityResource>
            {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> GetApiResorces =>
            new List<ApiResource>
            {
                new ApiResource("GYM.API")
            };
    }


}
