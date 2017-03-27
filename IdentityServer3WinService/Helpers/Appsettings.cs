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
        public const string SiliconClientIdKey = "SiliconClientId";
        public const string SiliconClientSecretKey = "SiliconClientSecret";
        public const string FrontendClientIdKey = "FrontendClientId";
        public const string UserKey = "user.@-@";
        public const string UserPasswordKey = "user.@-@.password";
        public const string SchemeKey = "scheme";
        public const string HostnameKey = "hostname";
        public const string PortKey = "port";
        public const string RedirectBackUrlKey = "after.auth.redirect.csv";
        public const string LogLevelKey = "log.level";

        public const string HumanIdtotkenLifetimeKey = "human.idtoken.lifetime.secs";
        public const string HumanAccesstotkenLifetimeKey = "human.accesstoken.lifetime.secs";

        public const string HumanCookieLifetimeKey = "human.cookie.lifetime.secs";
        
        public const string SiliconIdtotkenLifetimeKey = "silicon.idtoken.lifetime.secs";
        public const string SiliconAccesstotkenLifetimeKey = "silicon.accesstoken.lifetime.secs";
        //private const
        public static string HostUrl()
        {
            return string.Format("{0}://{1}:{2}/", Scheme(), Hostname(), Port());
        }

        public static string Scheme()
        {
            return ConfigurationManager.AppSettings.Get(SchemeKey);
        }
        public static string Port()
        {
            return ConfigurationManager.AppSettings.Get(PortKey);
        }
        public static string Hostname()
        {
            return ConfigurationManager.AppSettings.Get(HostnameKey);
        }

        public static List<string> RedirectBackUrlList()
        {
            return ConfigurationManager.AppSettings.Get(RedirectBackUrlKey).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        
        public static string LogLevel()
        {
            return ConfigurationManager.AppSettings.Get(LogLevelKey);
        }

        public static string SiliconClientId()
        {
            return ConfigurationManager.AppSettings.Get(SiliconClientIdKey);
        }

        public static string SiliconClientSecret()
        {
            return ConfigurationManager.AppSettings.Get(SiliconClientSecretKey);
        }
        public static string FrontendClientId()
        {
            return ConfigurationManager.AppSettings.Get(FrontendClientIdKey);
        }

        public static string User(string name)
        {
            return ConfigurationManager.AppSettings.Get(UserKey.Replace("@-@", name));
        }
        public static string UserPassword(string name)
        {
            return ConfigurationManager.AppSettings.Get(UserPasswordKey.Replace("@-@", name));
        }

        public static int HumanIdtokenLifetime()
        {
            return _getIntSetting(HumanIdtotkenLifetimeKey);
        }

        public static int HumanAccesstokenLifetime()
        {
            return _getIntSetting(HumanCookieLifetimeKey);
        }
        public static int HumanCookieLifetime()
        {
            return _getIntSetting(HumanCookieLifetimeKey);
        }

        public static int SiliconAccesstokenLifetime()
        {
            return _getIntSetting(SiliconAccesstotkenLifetimeKey);
        }

        public static int SiliconIdtokenLifetime()
        {
            return _getIntSetting(SiliconIdtotkenLifetimeKey);
        }

        private static int _getIntSetting(string name)
        {
            var val = ConfigurationManager.AppSettings.Get(name);
            return val != null ? Convert.ToInt16(val) : 3601;
        }
    }
}
