using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MadWare.Passbook
{
    public class PassGeneratorOptions
    {
        public X509Certificate2 AppleCert { get; set; }
        public X509Certificate2 PassCert { get; set; }
    }
}
