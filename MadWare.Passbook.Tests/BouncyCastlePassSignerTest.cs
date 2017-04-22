#if NETCOREAPP1_1

using MadWare.Passbook.PassSigner;
using System;
using System.IO;
using System.Text;
using Xunit;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace MadWare.Passbook.Tests
{
    public class BouncyCastlePassSignerTest
    {
        [Fact]
        public void BasicSign()
        {
            var sigener = new BouncyCastlePassSiger();
            byte[] TEST = Encoding.UTF8.GetBytes("{}");
            
            string path = typeof(BouncyCastlePassSignerTest).GetTypeInfo().Assembly.Location;
            var cwd = new FileInfo(path);
            path = cwd.Directory.FullName;

            var pass = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\pass.com.eon4u.p12"), "eon4u");
            var apple = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\AppleWWDRCA.cer"));

            var b = sigener.Sign(TEST, pass, apple);

        }
    }
}

# endif
