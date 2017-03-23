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
           // no human involved
            new Client
            {
                ClientName = "Silicon-only Client for site-site communication",
                ClientId = Appsettings.SiliconClientId(),
                Enabled = true,

                AccessTokenType     = AccessTokenType.Jwt,
                AccessTokenLifetime     = Appsettings.SiliconIdtotkenLifetime(),
                IdentityTokenLifetime   = Appsettings.SiliconAccesstotkenLifetime(),

                Flow = Flows.ClientCredentials,

                ClientSecrets = new List<Secret>
                {
                    new Secret(Appsettings.SiliconClientSecret().Sha256())
                },

                AllowedScopes = new List<string>
                {
                    IdSrv3.ScopeMvcFrontEnd, // for postback and AJAX call
                    IdSrv3.ScopeEntryQueueApi,
                    IdSrv3.ScopeNancyApi,
                    IdSrv3.ScopeServiceStackApi,
                    IdSrv3.ScopeWcfService
                }
            },
 
                new Client
                {
                    ClientName = "MVC Frontend Human",
                    ClientId = Appsettings.FrontendClientId(),
                    Enabled = true,

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = Appsettings.HumanIdtotkenLifetime(),
                    IdentityTokenLifetime = Appsettings.HumanAccesstotkenLifetime(),

                    //AbsoluteRefreshTokenLifetime=60,
                    //AuthorizationCodeLifetime=60,

                    Flow = Flows.Implicit,

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Roles,
                        IdSrv3.ScopeMcvFrontEndHuman
                    },
                    RedirectUris = Appsettings.RedirectBackUrlList(),
                    PostLogoutRedirectUris= Appsettings.RedirectBackUrlList(),
                    RequireConsent=false
                    //AllowRememberConsent =true

                }
            // human is involved
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