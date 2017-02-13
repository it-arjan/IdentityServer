using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLogWrapper;

namespace IdentityServer3WinService
{
    static class Program
    {
        static ILogger _logger = LogManager.CreateLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            AppDomain.CurrentDomain.FirstChanceException += LogException;
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }

         private static void LogException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            string msg = ex.Message;
            while (ex.InnerException != null)
            {
                msg = string.Format("{0}\n{1}", msg, ex.InnerException.Message);
                ex = ex.InnerException;
            }
            _logger.Error("FirstChanceException event raised in {0}: {1}", AppDomain.CurrentDomain.FriendlyName, msg);
        }
    }
}
