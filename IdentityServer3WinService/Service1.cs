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
            _logger.Debug("Checking config settings..");
            if (Helpers.Appsettings.SiliconClientId() == null) throw new Exception(MissingSetting("SiliconClientId"));
            if (Helpers.Appsettings.SiliconClientSecret() == null) throw new Exception(MissingSetting("SiliconClientSecret"));
            if (Helpers.Appsettings.FrontendClientId() == null) throw new Exception(MissingSetting(Helpers.Appsettings.FrontendClientIdKey));

            if (Helpers.Appsettings.Hostname() == null) throw new Exception(MissingSetting(Helpers.Appsettings.HostnameKey));
            if (Helpers.Appsettings.Port() == null) throw new Exception(MissingSetting(Helpers.Appsettings.PortKey));
            if (Helpers.Appsettings.Scheme() == null) throw new Exception(MissingSetting(Helpers.Appsettings.SchemeKey));
            if (Helpers.Appsettings.RedirectBackUrl() == null) throw new Exception(MissingSetting(Helpers.Appsettings.RedirectBackUrlKey));

            _logger.Debug("config setting seem ok..");
            _logger.Debug("Url = {0}", Helpers.Appsettings.HostUrl());
            _logger.Debug("{0} = {1}", Helpers.Appsettings.RedirectBackUrlKey, Helpers.Appsettings.RedirectBackUrl());
            _logger.Debug("..done with config checks");
        }
        private string MissingSetting(string setting)
        {
            return string.Format("setting {0} is not present in app.config");
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
