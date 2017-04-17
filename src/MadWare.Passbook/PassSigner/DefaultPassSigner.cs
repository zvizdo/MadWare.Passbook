using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MadWare.Passbook.PassStyle;

namespace MadWare.Passbook.PassSigner
{
    public class DefaultPassSigner : IPassSigner
    {
        public byte[] Sign(byte[] manifest, X509Certificate2 passCert, X509Certificate2 appleCert)
        {
            ContentInfo contentInfo = new ContentInfo(manifestFile);

            SignedCms signing = new SignedCms(contentInfo, true);

            CmsSigner signer = new CmsSigner(SubjectIdentifierType.SubjectKeyIdentifier, passCert)
            {
                IncludeOption = X509IncludeOption.None
            };

            Trace.TraceInformation("Fetching Apple Certificate for signing..");
            Trace.TraceInformation("Constructing the certificate chain..");
            signer.Certificates.Add(appleCert);
            signer.Certificates.Add(passCert);

            signer.SignedAttributes.Add(new Pkcs9SigningTime());

            Trace.TraceInformation("Processing the signature..");
            signing.ComputeSignature(signer);

            signatureFile = signing.Encode();
        }

        public bool ValidateCertificate<T>(X509Certificate2 passCert, Pass<T> p) where T : BasePassStyle
        {
            throw new NotImplementedException();
        }
    }
}
