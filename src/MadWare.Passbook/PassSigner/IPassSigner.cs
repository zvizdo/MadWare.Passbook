using MadWare.Passbook.PassStyle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MadWare.Passbook.PassSigner
{
    public interface IPassSigner
    {
        bool ValidateCertificate<T>(X509Certificate2 passCert, Pass<T> p) where T : BasePassStyle;

        byte[] Sign(byte[] manifest, X509Certificate2 passCert, X509Certificate2 appleCert);
    }
}
