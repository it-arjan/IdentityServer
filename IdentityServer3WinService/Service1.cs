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
        private ILogger _logger = LogManager.CreateLogger(typeof(Service1), Configsettings.LogLevel());
        public Service1()
        { 
            InitializeComponent();
        }

        private void CheckHealth()
        {
            _logger.Info("=================== Checking config settings..");
            SettingsChecker.CheckPresenceAllPlainSettings(typeof(Configsettings));

            _logger.Info("config setting seem ok..");
            _logger.Info("Url = {0}", Configsettings.HostUrl());

            _logger.Info("HUMAN Token Life times: human-access={0}, human-id={1}, human cookie={2}", Configsettings.HumanAccesstokenLifetime(), Configsettings.HumanIdtokenLifetime(), Configsettings.HumanCookieLifetime());
            _logger.Info("Silicon Token Life time: {0}", Configsettings.SiliconAccesstokenLifetime());

            _logger.Info("{0} = {1}", Configsettings.RedirectBackUrlKey, string.Join(",", Configsettings.RedirectBackUrlList()));
            _logger.Info("..done with config checks");
        }
 
        protected override void OnStart(string[] args)
        {
            _logger.Info("=========== Starting http auth Service =============");

            CheckHealth();
            // web options doesn't work
            var url = Configsettings.HostUrl();
            _httpServer = WebApp.Start<Startup>(url);
        }

        protected override void OnStop()
        {
            _logger.Info("=========== Stopping http auth Service =============");
            //_httpServer.Dispose(); // no need
        }
    }
}
