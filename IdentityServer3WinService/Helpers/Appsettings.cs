using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace IdentityServer3WinService.Helpers
{
    static class Appsettings
    {
        public static string HostUrl()
        {
            var key = HostKey();
            return ConfigurationManager.AppSettings.Get(key);
        }

        public static string HostKey()
        {
            return string.Format("facing.{0}.hostname", ConfigurationManager.AppSettings.Get("facing").ToLower());
        }

        public static string RedirectBackUrl()
        {
            var key = RedirectBackUrlKey();
            return ConfigurationManager.AppSettings.Get(key);
        }

        public static string RedirectBackUrlKey()
        {
            return string.Format("facing.{0}.redirectback.to", ConfigurationManager.AppSettings.Get("facing").ToLower());
        }

        public static string SiliconClientId()
        {
            return ConfigurationManager.AppSettings.Get("SiliconClientId");
        }

        public static string SiliconClientSecret()
        {
            return ConfigurationManager.AppSettings.Get("SiliconClientSecret");
        }
        public static string FrontendClientId()
        {
            return ConfigurationManager.AppSettings.Get("FrontendClientId");
        }

        public static string FrontendClientSecret()
        {
            return ConfigurationManager.AppSettings.Get("FrontendClientSecret");
        }
    }
}
