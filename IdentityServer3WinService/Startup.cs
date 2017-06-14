using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owin;
using IdentityServer3.Core.Configuration;
using IdentityServer3WinService.Helpers;

using NLogWrapper;
using IdentityServer3.Core.Services;

namespace IdentityServer3WinService
{
    public class Startup
    {
        private ILogger _logger = LogManager.CreateLogger(typeof(Startup), Configsettings.LogLevel());
        public void Configuration(IAppBuilder app)
        {

            string certificateSubject = Configsettings.Hostname();
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),
                

                AuthenticationOptions = new AuthenticationOptions
                {
                    EnablePostSignOutAutoRedirect = true,
                    CookieOptions = new CookieOptions
                    {
                        AllowRememberMe = Configsettings.HumanAllowRemember(),
                        ExpireTimeSpan = TimeSpan.FromSeconds(Configsettings.HumanCookieLifetime())
                    }
               },
                RequireSsl = false,
                SigningCertificate = Config.Certificate.Get(certificateSubject),
                EnableWelcomePage = true,
            };
            options.Factory.CorsPolicyService = new Registration<ICorsPolicyService>(typeof(CorsPolicy));
            app.UseIdentityServer(options);
        }

    }
}