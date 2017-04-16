using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MadWare.Passbook
{
    public class PassGeneratorOptions
    {
        private X509Certificate2 AppleCert { get; set; }
        private X509Certificate2 PassCert { get; set; }
    }
}
