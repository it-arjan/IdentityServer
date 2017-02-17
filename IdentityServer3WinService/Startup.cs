using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owin;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using NLogWrapper;

namespace IdentityServer3WinService
{
    public class Startup
    {
        private ILogger _logger = LogManager.CreateLogger(typeof(Startup));
        public void Configuration(IAppBuilder app)
        {
            string subject = Helpers.Appsettings.Hostname();
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
                        ExpireTimeSpan = TimeSpan.FromMinutes(5)
                    }
               },
                
                SigningCertificate = Config.Certificate.Get(subject),
                EnableWelcomePage = true,
                RequireSsl = true
            };
            app.UseIdentityServer(options);
        }
    }
}