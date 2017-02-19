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
            return string.Format("{0}://{1}:{2}/", Scheme(), Hostname(), Port());
        }

        public static string Scheme()
        {
            return ConfigurationManager.AppSettings.Get(SchemeKey());
        }
        public static string Port()
        {
            return ConfigurationManager.AppSettings.Get(PortKey());
        }
        public static string Hostname()
        {
            return ConfigurationManager.AppSettings.Get(HostnameKey());
        }

        public static string SchemeKey()
        {
            return "scheme";
        }
        public static string HostnameKey()
        {
            return "hostname";
        }
        public static string PortKey()
        {
            return "port";
        }

        public static string RedirectBackUrl()
        {
            return ConfigurationManager.AppSettings.Get(RedirectBackUrlKey());
        }

        public static string RedirectBackUrlKey()
        {
            return "after.auth.redirect";
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
