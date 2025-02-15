﻿using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
            new ApiResource("alevelwebsite.com")
            {
                Scopes = new List<Scope>
                {
                    new Scope("mvc")
                },
            },
            new ApiResource("catalog")
            {
                Scopes = new List<Scope>
                {
                    new Scope("catalog.catalogbrand"),
                    new Scope("catalog.catalogitem"),
                    new Scope("catalog.catalogtype")
                },
            }
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
            new Client
            {
                ClientId = "react_spa",
                ClientName = "React SPA",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { $"{configuration["ReactClientUrl"]}/callback" },
                PostLogoutRedirectUris = { $"{configuration["ReactClientUrl"]}/logout-callback" },
                AllowedCorsOrigins = { configuration["ReactClientUrl"] },
                AllowedScopes = { "openid", "profile", "mvc" },
                AllowAccessTokensViaBrowser = true
            },
            new Client
            {
                ClientId = "mvc_pkce",
                ClientName = "MVC PKCE Client",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = {new Secret("secret".Sha256())},
                RedirectUris = { $"{configuration["MvcUrl"]}/signin-oidc"},
                AllowedScopes = {"openid", "profile", "mvc"},
                RequirePkce = true,
                RequireConsent = false
            },
            new Client
            {
                ClientId = "catalog",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes =
                {
                    "catalog.catalogitem"
                }
            },
            new Client
            {
                ClientId = "catalogswaggerui",
                ClientName = "Catalog Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { $"{configuration["CatalogApi"]}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{configuration["CatalogApi"]}/swagger/" },
                AllowedScopes =
                {
                    "mvc", "catalog.catalogitem"
                }
            },
            new Client
            {
                ClientId = "basket",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes =
                {
                    "catalog.catalogitem"
                }
            },
            new Client
            {
                ClientId = "basketswaggerui",
                ClientName = "Basket Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { $"{configuration["BasketApi"]}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{configuration["BasketApi"]}/swagger/" },
                AllowedScopes =
                {
                    "mvc"
                }
            },
        };
        }
    }
}