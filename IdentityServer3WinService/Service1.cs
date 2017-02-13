using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using NLogWrapper;
using System.Configuration;

namespace IdentityServer3WinService
{
    public partial class Service1 : ServiceBase
    {
        private IDisposable _httpServer;
        private ILogger _logger = LogManager.CreateLogger(typeof(Service1));
        public Service1()
        { 
            InitializeComponent();
        }

        private void CheckHealth()
        {
            if (Helpers.Appsettings.SiliconClientId() == null) throw new Exception("setting 'SiliconClientId' is not present in app.config");
            if (Helpers.Appsettings.SiliconClientSecret() == null) throw new Exception("setting 'SiliconClientSecret' is not present in app.config");
            if (Helpers.Appsettings.FrontendClientId() == null) throw new Exception("setting 'FrontendClientId' is not present in app.config");
            if (Helpers.Appsettings.FrontendClientSecret() == null) throw new Exception("setting 'FrontendClientSecret' is not present in app.config");

            if (ConfigurationManager.AppSettings.Get("facing") == null) throw new Exception("setting 'facing' is not present in app.config");

            if (Helpers.Appsettings.HostUrl() == null) throw new Exception(Helpers.Appsettings.HostKey() + " is not present in app.config");
            if (Helpers.Appsettings.RedirectBackUrl() == null) throw new Exception(Helpers.Appsettings.RedirectBackUrlKey() + " is not present in app.config");

            _logger.Debug("facing = {0}", ConfigurationManager.AppSettings.Get("facing"));
            _logger.Debug("{0} = {1}", Helpers.Appsettings.HostKey(), ConfigurationManager.AppSettings.Get(Helpers.Appsettings.HostKey()));
            _logger.Debug("{0} = {1}", Helpers.Appsettings.RedirectBackUrlKey(), ConfigurationManager.AppSettings.Get(Helpers.Appsettings.RedirectBackUrlKey()));
            _logger.Debug("-");
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("=========== Starting http auth Service =============");

            CheckHealth();
            // web options doesn't work
            var url = Helpers.Appsettings.HostUrl();
            _httpServer = WebApp.Start<Startup>(url);
            _logger.Info("Listening on {0}", url);
        }

        protected override void OnStop()
        {
            _logger.Info("=========== Stopping http auth Service =============");
            //_httpServer.Dispose(); // no need
        }
    }
}
