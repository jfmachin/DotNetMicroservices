﻿using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer {
    public class Config {
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
            new ApiScope("orderAPI", "Order API")
        };
        public static IEnumerable<Client> Clients => new Client[] {
            new Client {
                ClientId = "orderClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "orderAPI" }
            }
        };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {};
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {};
        public static List<TestUser> TestUsers => new List<TestUser> {};
    }
}
