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

            var privateKey = this.GetPrivateKey(passCert.GetRSAPrivateKey().ExportParameters(true));
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

        private RsaPrivateCrtKeyParameters GetPrivateKey(
          RSAParameters rp)
        {
            return new RsaPrivateCrtKeyParameters(
                new BigInteger(1, rp.Modulus),
                new BigInteger(1, rp.Exponent),
                new BigInteger(1, rp.D),
                new BigInteger(1, rp.P),
                new BigInteger(1, rp.Q),
                new BigInteger(1, rp.DP),
                new BigInteger(1, rp.DQ),
                new BigInteger(1, rp.InverseQ));
        }

        public static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privKey)
        {
            RSAParameters rp = new RSAParameters();
            rp.Modulus = privKey.Modulus.ToByteArrayUnsigned();
            rp.Exponent = privKey.PublicExponent.ToByteArrayUnsigned();
            rp.P = privKey.P.ToByteArrayUnsigned();
            rp.Q = privKey.Q.ToByteArrayUnsigned();
            rp.D = ConvertRSAParametersField(privKey.Exponent, rp.Modulus.Length);
            rp.DP = ConvertRSAParametersField(privKey.DP, rp.P.Length);
            rp.DQ = ConvertRSAParametersField(privKey.DQ, rp.Q.Length);
            rp.InverseQ = ConvertRSAParametersField(privKey.QInv, rp.Q.Length);
            return rp;
        }

        private static byte[] ConvertRSAParametersField(BigInteger n, int size)
        {
            byte[] bs = n.ToByteArrayUnsigned();
            if (bs.Length == size)
                return bs;
            if (bs.Length > size)
                throw new ArgumentException("Specified size too small", "size");
            byte[] padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
            return padded;
        }

    }
}

#endif