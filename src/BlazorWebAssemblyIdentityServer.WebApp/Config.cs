// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace BlazorWebAssemblyIdentityServer.WebApp
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new("datareadonly"),
                new("datamanipulation"),
            };

        public static IEnumerable<IdentityServer4.Models.Client> CreateClients(string applicationUrl)
        {
            List<IdentityServer4.Models.Client> clients = new()
            {
                new()
                {
                    ClientId = "BlazorWebAssemblyClient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        Flurl.Url.Combine(
                            applicationUrl,
                            "authentication",
                            "login-callback")
                    },
                    PostLogoutRedirectUris =
                    {
                        Flurl.Url.Combine(
                            applicationUrl,
                            "authentication",
                            "logout-callback")
                    },
                    AllowOfflineAccess = false,
                    AllowedCorsOrigins =
                    {
                        applicationUrl
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "datareadonly",
                        "datamanipulation"
                    }
                }
            };


            return clients.ToArray();
        }
    }
}