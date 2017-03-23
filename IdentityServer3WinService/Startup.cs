using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owin;
using IdentityServer3.Core.Configuration;
using IdentityServer3WinService.Helpers;

using NLogWrapper;

namespace IdentityServer3WinService
{
    public class Startup
    {
        private ILogger _logger = LogManager.CreateLogger(typeof(Startup), Appsettings.LogLevel());
        public void Configuration(IAppBuilder app)
        {
            string certificateSubject = Appsettings.Hostname();
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),

                AuthenticationOptions= new AuthenticationOptions
                {
                    EnablePostSignOutAutoRedirect = true,
                    CookieOptions = new CookieOptions
                    {
                        AllowRememberMe = false,
                        ExpireTimeSpan = TimeSpan.FromMinutes(15 * IdSrv3.SessionSetting)
                    }
               },
                
                SigningCertificate = Config.Certificate.Get(certificateSubject),
                EnableWelcomePage = true,
            };
            app.UseIdentityServer(options);
        }
    }
}