using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using NLogWrapper;
using IdentityServer3WinService.Helpers;

namespace IdentityServer3WinService
{
    public partial class Service1 : ServiceBase
    {
        private IDisposable _httpServer;
        private ILogger _logger = LogManager.CreateLogger(typeof(Service1), Appsettings.LogLevel());
        public Service1()
        { 
            InitializeComponent();
        }

        private void CheckHealth()
        {
            _logger.Info("Checking config settings..");
            if (Appsettings.SiliconClientId() == null) throw new Exception(MissingSetting("SiliconClientId"));
            if (Appsettings.SiliconClientSecret() == null) throw new Exception(MissingSetting("SiliconClientSecret"));
            if (Appsettings.FrontendClientId() == null) throw new Exception(MissingSetting(Appsettings.FrontendClientIdKey));

            if (Appsettings.Hostname() == null) throw new Exception(MissingSetting(Appsettings.HostnameKey));
            if (Appsettings.Port() == null) throw new Exception(MissingSetting(Appsettings.PortKey));
            if (Appsettings.Scheme() == null) throw new Exception(MissingSetting(Appsettings.SchemeKey));
            if (Appsettings.RedirectBackUrlList() == null) throw new Exception(MissingSetting(Appsettings.RedirectBackUrlKey));

            _logger.Info("config setting seem ok..");
            _logger.Info("Url = {0}", Appsettings.HostUrl());
            _logger.Info("{0} = {1}", Appsettings.RedirectBackUrlKey, string.Join(",", Appsettings.RedirectBackUrlList()));
            _logger.Info("..done with config checks");
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
            var url = Appsettings.HostUrl();
            _httpServer = WebApp.Start<Startup>(url);
        }

        protected override void OnStop()
        {
            _logger.Info("=========== Stopping http auth Service =============");
            //_httpServer.Dispose(); // no need
        }
    }
}
