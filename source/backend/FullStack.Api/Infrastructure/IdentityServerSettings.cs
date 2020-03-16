using System;
using System.Linq;
using IdentityServer4;
using IdentityServer4.Models;
using System.Security.Claims;
using FullStack.Core.Helpers;
using System.Collections.Generic;
using FullStack.Domain.Constants;
using FullStack.Domain.Infrastructure.Config;

namespace FullStack.Api.Infrastructure
{
    public sealed class ApiClient
    {
        #region Public Properties

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int AccessTokenLifetime { get; set; }

        #endregion
    }

    public static class IdentityServerSettings
    {
        #region Public Static Methods

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource(IdentityServerApiConstants.ResourceName, IdentityServerApiConstants.ResourceDisplayName)
                {
                    Scopes = {
                        new Scope(IdentityServerConstants.StandardScopes.OpenId),
                        new Scope(IdentityServerConstants.StandardScopes.Profile),
                    },
                    UserClaims = {
                        ClaimTypes.Name,
                        ClaimTypes.Role,
                        ClaimTypes.Email,
                        ClaimTypes.GivenName,
                        ClaimTypes.Surname,
                        IdentityServerApiConstants.StandardClaimsConstants.RoleId
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            var output = new List<Client>();
            ConfigHelper.TryGet<IdentityServerConfig>(nameof(IdentityServerConfig), out var configuration);

            if (configuration.Clients != null && configuration.Clients.Any())
            {
                // ApiClient - FrontendAPP
                var frontendApp = configuration.Clients.FirstOrDefault(x => x.ClientId.Equals("frontend.app"));
                if (frontendApp != null)
                {
                    output.Add(new Client
                    {
                        ClientId = frontendApp.ClientId,
                        ClientSecrets = { new Secret(frontendApp.ClientSecret.Sha256()) },
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        AllowOfflineAccess = true,
                        AllowedScopes = {
                            IdentityServerApiConstants.ResourceName,
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.OfflineAccess,
                        },
                        RefreshTokenUsage = TokenUsage.ReUse,
                        RefreshTokenExpiration = TokenExpiration.Sliding,
                        AccessTokenLifetime = frontendApp.AccessTokenLifetime
                    });
                }

                // TODO: se necessário, acrescentar aqui os demais clients 
            }

            return output;
        }

        #endregion
    }
}
