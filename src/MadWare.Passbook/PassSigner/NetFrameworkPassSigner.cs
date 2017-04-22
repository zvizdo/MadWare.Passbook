#if NET46

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MadWare.Passbook.PassStyle;
using System.Security.Cryptography.Pkcs;

namespace MadWare.Passbook.PassSigner
{
    public class NetFrameworkPassSigner : IPassSigner
    {
        private const string PASS_TYPE_PREFIX = "Pass Type ID: ";

        public byte[] Sign(byte[] manifest, X509Certificate2 passCert, X509Certificate2 appleCert)
        {
            ContentInfo contentInfo = new ContentInfo(manifest);

            SignedCms signing = new SignedCms(contentInfo, true);

            CmsSigner signer = new CmsSigner(SubjectIdentifierType.SubjectKeyIdentifier, passCert)
            {
                IncludeOption = X509IncludeOption.None
            };

            signer.Certificates.Add(appleCert);
            signer.Certificates.Add(passCert);

            signer.SignedAttributes.Add(new Pkcs9SigningTime());

            signing.ComputeSignature(signer);

            return signing.Encode();
        }

        public bool ValidateCertificate<T>(X509Certificate2 passCert, Pass<T> p) where T : BasePassStyle
        {
            string passTypeIdentifier = passCert.GetNameInfo(X509NameType.SimpleName, false);

            if (passTypeIdentifier.StartsWith(PASS_TYPE_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                passTypeIdentifier = passTypeIdentifier.Substring(PASS_TYPE_PREFIX.Length);

                if (!string.IsNullOrEmpty(passTypeIdentifier) && !string.Equals(p.PassTypeIdentifier, passTypeIdentifier, StringComparison.Ordinal))
                {
                    if (!string.IsNullOrEmpty(p.PassTypeIdentifier))
                    {
                        throw new ArgumentException(
                            string.Format("Configured passTypeIdentifier {0} does not match pass certificate {1}. If passTypeIndentifier is left null, it will be automatically assigned.", p.PassTypeIdentifier, passTypeIdentifier)
                            );
                    }

                    p.PassTypeIdentifier = passTypeIdentifier;
                }
            }
            else
            {
                throw new InvalidOperationException("Wrong pass certificate - does not have Pass Type ID field.");
            }

            Dictionary<string, string> nameParts =
            passCert.SubjectName.Name
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .ToDictionary(k => k.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[0],
                    e => e.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1]);

            string teamIdentifier;
            if (nameParts.TryGetValue("OU", out teamIdentifier))
            {
                if (!string.IsNullOrEmpty(teamIdentifier) && !string.Equals(p.TeamIdentifier, teamIdentifier, StringComparison.Ordinal))
                {
                    if (!string.IsNullOrEmpty(p.TeamIdentifier))
                    {
                        throw new ArgumentException(
                            string.Format("Configured teamIdentifier {0} does not match pass certificate {1}. If teamIdentifier is left null, it will be automatically assigned.", p.TeamIdentifier, teamIdentifier)
                            );
                    }

                    p.TeamIdentifier = teamIdentifier;
                }
            }
            else
            {
                throw new InvalidOperationException("Wrong pass certificate - does not have Team Identifier field.");
            }

            return true;
        }
    }
}

# endif
