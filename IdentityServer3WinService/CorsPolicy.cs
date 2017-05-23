using IdentityServer3.Core.Services;
using IdentityServer3WinService.Helpers;
using NLogWrapper;
using System.Threading.Tasks;

namespace IdentityServer3WinService
{
    public class CorsPolicy : ICorsPolicyService
    {
        private ILogger _logger = LogManager.CreateLogger(typeof(Startup), Appsettings.LogLevel());
        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            foreach (var url in Appsettings.RedirectBackUrlList())
            {
                if (url.Contains(origin))
                    return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
