using MadWare.Passbook.PassSigner;
using System;
using System.IO;
using System.Text;
using Xunit;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace MadWare.Passbook.Tests
{
    public class NetFrameworkPassSignerTests
    {
        [Fact]
        public void Test1()
        {
            var signer = new NetFrameworkPassSigner();
            byte[] testPass = Encoding.UTF8.GetBytes("{}");

            string path = AppDomain.CurrentDomain.BaseDirectory; //Assembly.GetExecutingAssembly().Location;
            var cwd = new DirectoryInfo(path).Parent.Parent.Parent;
            path = cwd.FullName;

            var pass = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\pass.com.eon4u.p12"), "eon4u");
            var apple = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\AppleWWDRCA.cer"));


            var b = signer.Sign(testPass, pass, apple);

        }
    }
}
