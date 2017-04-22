#if NETSTANDARD1_6

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.X509;
using MadWare.Passbook.PassStyle;
using Org.BouncyCastle.Crypto;
using System.Security.Cryptography;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Cms;
using System.Collections;
using Org.BouncyCastle.X509.Store;

namespace MadWare.Passbook.PassSigner
{
    public class BouncyCastlePassSiger : IPassSigner
    {
        public byte[] Sign(byte[] manifest, X509Certificate2 passCert, X509Certificate2 appleCert)
        {
            Org.BouncyCastle.X509.X509Certificate apple = new X509CertificateParser().ReadCertificate(appleCert.RawData);
            Org.BouncyCastle.X509.X509Certificate pass = new X509CertificateParser().ReadCertificate(passCert.RawData);

            var privateKey = this.GetRsaKeyPair(passCert.GetRSAPrivateKey().ExportParameters(true)).Private;
            var generator = new CmsSignedDataGenerator();
            generator.AddSigner(privateKey, pass, CmsSignedGenerator.DigestSha1);

            IList certList = new List<Org.BouncyCastle.X509.X509Certificate> { pass, apple };
            X509CollectionStoreParameters storeParameters = new X509CollectionStoreParameters(certList);
            IX509Store store509 = X509StoreFactory.Create("CERTIFICATE/COLLECTION", storeParameters);

            generator.AddCertificates(store509);

            var content = new CmsProcessableByteArray(manifest);
            var signature = generator.Generate(content, false).GetEncoded();

            return signature;
        }

        public bool ValidateCertificate<T>(X509Certificate2 passCert, Pass<T> p) where T : BasePassStyle
        {
            throw new NotImplementedException();
        }

        private AsymmetricCipherKeyPair GetRsaKeyPair(
          RSAParameters rp)
        {
            BigInteger modulus = new BigInteger(1, rp.Modulus);
            BigInteger pubExp = new BigInteger(1, rp.Exponent);

            RsaKeyParameters pubKey = new RsaKeyParameters(
                false,
                modulus,
                pubExp);

            RsaPrivateCrtKeyParameters privKey = new RsaPrivateCrtKeyParameters(
                modulus,
                pubExp,
                new BigInteger(1, rp.D),
                new BigInteger(1, rp.P),
                new BigInteger(1, rp.Q),
                new BigInteger(1, rp.DP),
                new BigInteger(1, rp.DQ),
                new BigInteger(1, rp.InverseQ));

            return new AsymmetricCipherKeyPair(pubKey, privKey);
        }

    }
}

# endif