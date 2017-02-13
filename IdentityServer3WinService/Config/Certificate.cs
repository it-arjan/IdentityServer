using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServer3WinService.Config
{
    static class Certificate
    {
        public static X509Certificate2 Get(string certName)
        {
            return GetCertificateFromStore(certName);
            //return GetCertificateFromDisk();
        }

        private static X509Certificate2 GetCertificateFromStore(string name)
        {
            X509Certificate2 result = null;
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            try
            {
                var certCollection = store.Certificates;

                foreach (var cert in certCollection)
                {
                    //Console.WriteLine("cert name: {0}", cert.Subject);
                    if (cert.Subject.Contains(name))
                    {
                        result = cert;
                        break;
                    }
                }
            }
            finally
            {
                store.Close();
            }
            if (result == null) throw new Exception(string.Format("certificate with CN containing '{0}' not found in My store", name));
            return result;
        }

    }
}
