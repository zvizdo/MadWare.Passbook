using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MadWare.Passbook
{
    public static class Helpers
    {

        public static X509Certificate2 LoadCertificateFromBytes(byte[] bytes, string password = null)
        {
            X509Certificate2 certificate = null;

            if (password == null)
            {
                certificate = new X509Certificate2(bytes);
            }
            else
            {
                X509KeyStorageFlags flags = X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable;
                certificate = new X509Certificate2(bytes, password, flags);
            }

            return certificate;
        }

    }
}
