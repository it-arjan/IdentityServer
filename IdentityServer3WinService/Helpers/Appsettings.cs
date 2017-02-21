﻿using System;
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
        public const string UserBobKey = "user.bob";
        public const string UserBobPasswordKey = "user.bob.password";
        public const string SchemeKey = "scheme";
        public const string HostnameKey = "hostname";
        public const string PortKey = "port";
        public const string RedirectBackUrlKey = "after.auth.redirect";

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

        public static string RedirectBackUrl()
        {
            return ConfigurationManager.AppSettings.Get(RedirectBackUrlKey);
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

        public static string UserBob()
        {
            return ConfigurationManager.AppSettings.Get(UserBobKey);
        }
        public static string UserBobPassword()
        {
            return ConfigurationManager.AppSettings.Get(UserBobPasswordKey);
        }
    }
}
