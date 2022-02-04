using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> identityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> scopes =>
            new List<ApiScope>
            {
                new ApiScope("mango","Mango Server"),
                new ApiScope(name:"Read", displayName:"Read your data"),
                new ApiScope(name:"Write", displayName:"Write your data"),
                new ApiScope(name:"Delete", displayName:"Delete your data")
            };

        public static IEnumerable<Client> clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId ="client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"Read", "Write", "Delete"}
                },
                new Client
                {
                    ClientId ="mango",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris={"https://localhost:7161/signin-oidc"},
                    PostLogoutRedirectUris={"https://localhost:7161/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mango"
                    }
                }
            };
    }
}
