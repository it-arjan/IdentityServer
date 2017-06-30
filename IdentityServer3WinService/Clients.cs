using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdentityServer3.Core.Models;
using IdentityServer3.Core;
using IdentityServer3WinService.Helpers;

namespace IdentityServer3WinService
{
    static class Clients
    {
        public static List<Client> Get()
        {

            return new List<Client>
        {
                // Human
            new Client
            {
                ClientName = "MVC Frontend Human",
                ClientId = Configsettings.FrontendClientId(),
                Enabled = true,

                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = Configsettings.HumanAccesstokenLifetime(),
                IdentityTokenLifetime = Configsettings.HumanIdtokenLifetime(),

                //AbsoluteRefreshTokenLifetime=60,
                //AuthorizationCodeLifetime=60,

                Flow = Flows.Implicit,

                AllowedScopes = new List<string>
                {
                    Constants.StandardScopes.OpenId,
                    Constants.StandardScopes.Roles,
                    IdSrv3.ScopeMcvFrontEndHuman
                },
                RedirectUris = Configsettings.RedirectBackUrlList(),
                PostLogoutRedirectUris= Configsettings.RedirectBackUrlList(),
                RequireConsent=false
                //AllowRememberConsent =true

            },
           // Silicon, no human involved
            new Client
            {
                ClientName = "Silicon-only Client for site-site communication",
                ClientId = Configsettings.SiliconClientId(),
                Enabled = true,

                AccessTokenType     = AccessTokenType.Jwt,
                AccessTokenLifetime     = Configsettings.SiliconAccesstokenLifetime(),
                //IdentityTokenLifetime   = Appsettings.SiliconIdtokenLifetime(),

                Flow = Flows.ClientCredentials,

                ClientSecrets = new List<Secret>
                {
                    new Secret(Configsettings.SiliconClientSecret().Sha256())
                },

                AllowedScopes = new List<string>
                {
                    IdSrv3.ScopeMvcFrontEnd, 
                    IdSrv3.ScopeEntryQueueApi,
                    IdSrv3.ScopeNancyApi,
                    IdSrv3.ScopeFrontendDataApi,
                    IdSrv3.ScopeServiceStackApi,
                    IdSrv3.ScopeMsWebApi
                }
            },
           // AutoTest client, no human involved
            new Client
            {
                ClientName = "Silicon client for AutoTest",
                ClientId = Configsettings.AutoTestClientId(),
                Enabled = true,

                AccessTokenType     = AccessTokenType.Jwt,
                AccessTokenLifetime     = Configsettings.SiliconAccesstokenLifetime(),
                //IdentityTokenLifetime   = Appsettings.SiliconIdtokenLifetime(),

                Flow = Flows.ClientCredentials,

                ClientSecrets = new List<Secret>
                {
                    new Secret(Configsettings.AutoTestClientSecret().Sha256())
                },

                AllowedScopes = new List<string>
                {
                    IdSrv3.ScopeMvcFrontEnd, 
                    IdSrv3.ScopeEntryQueueApi,
                    IdSrv3.ScopeNancyApi,
                    IdSrv3.ScopeServiceStackApi,
                    IdSrv3.ScopeMsWebApi
                }
            }
            // Ailicon, human is involved
                //new Client
                //{
                //    ClientName = "Mvc Ajax On Behalf of a User",
                //    ClientId = "mvcAjax",
                //    Enabled = false,

                //    AccessTokenType = AccessTokenType.Jwt,
                //    AccessTokenLifetime = 900 * IdSrv3.SessionSetting,
                //    IdentityTokenLifetime=900 * IdSrv3.SessionSetting,

                //    Flow = Flows.ResourceOwner,

                //    ClientSecrets = new List<Secret>
                //    {
                //        new Secret("mvcAjax".Sha256())
                //    },

                //    //AllowAccessToAllScopes = true
                //    AllowedScopes = new List<string>
                //    {
                //        Constants.StandardScopes.OpenId,
                //        Constants.StandardScopes.Roles,
                //        IdSrv3.ScopeMvcFrontEnd,
                //   }
                //},
            };
        }
    }
}