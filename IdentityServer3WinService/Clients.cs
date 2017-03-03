using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdentityServer3.Core.Models;
using IdentityServer3.Core;

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
                ClientId = Helpers.Appsettings.SiliconClientId(),
                Enabled = true,

                AccessTokenType     = AccessTokenType.Jwt,
                AccessTokenLifetime     = 300 * Helpers.IdSrv3.SessionSetting,
                IdentityTokenLifetime   = 300 * Helpers.IdSrv3.SessionSetting,

                Flow = Flows.ClientCredentials,

                ClientSecrets = new List<Secret>
                {
                    new Secret(Helpers.Appsettings.SiliconClientSecret().Sha256())
                },

                AllowedScopes = new List<string>
                {
                    Helpers.IdSrv3.ScopeMvcFrontEnd, // for postback and AJAX call
                    Helpers.IdSrv3.ScopeEntryQueueApi,
                    Helpers.IdSrv3.ScopeNancyApi,
                    Helpers.IdSrv3.ScopeServiceStackApi,
                    Helpers.IdSrv3.ScopeWcfService
                }
            },
             // human is involved
                //new Client
                //{
                //    ClientName = "Web Entrypoint On Behalf of a User",
                //    ClientId = "webentrypoint_onbehalf",
                //    Enabled = true,
                //    AccessTokenType = AccessTokenType.Jwt,
                //    Flow = Flows.ResourceOwner,

                //    ClientSecrets = new List<Secret>
                //    {
                //        new Secret("21B5F798-BEXY-42ZT-8568-0425B9DSDC3B".Sha256())
                //    },

                //    //AllowAccessToAllScopes = true
                //    AllowedScopes = new List<string>
                //    {
                //        Constants.StandardScopes.OpenId,
                //        Constants.StandardScopes.Roles,
                //        "MvcFrontEnd",
                //        "NancyApi",
                //        "ServiceStackApi",
                //        "WcfService"
                //   }
                //},
                new Client
                {
                    ClientName = "MVC Frontend",
                    ClientId = Helpers.Appsettings.FrontendClientId(),
                    Enabled = true,

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 900 * Helpers.IdSrv3.SessionSetting,
                    IdentityTokenLifetime=900 * Helpers.IdSrv3.SessionSetting,

                    //AbsoluteRefreshTokenLifetime=60,
                    //AuthorizationCodeLifetime=60,

                    Flow = Flows.Implicit,

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Roles,
                        Helpers.IdSrv3.ScopeMcvFrontEndHuman
                    },
                    RedirectUris = new List<string>
                    {
                        Helpers.Appsettings.RedirectBackUrl()
                    },
                    PostLogoutRedirectUris= new List<string>
                    {
                        Helpers.Appsettings.RedirectBackUrl()
                    },

                    RequireConsent=false
                    //AllowRememberConsent =true

                }
            };
        }
    }
}