using MadWare.Passbook.PassSigner;
using System;
using System.IO;
using System.Text;
using Xunit;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace MadWare.Passbook.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var sigener = new BouncyCastlePassSiger();
            byte[] TEST = Encoding.UTF8.GetBytes("abc");
            
            string path = typeof(UnitTest1).GetTypeInfo().Assembly.Location;
            var cwd = new FileInfo(path);
            path = cwd.Directory.FullName;

            System.Security.Cryptography.X509Certificates.X509Certificate2 pass = new System.Security.Cryptography.X509Certificates.X509Certificate2(File.ReadAllBytes(path+"\\pass.com.eon4u.p12"), "eon4u", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
            System.Security.Cryptography.X509Certificates.X509Certificate2 apple = new System.Security.Cryptography.X509Certificates.X509Certificate2(File.ReadAllBytes(path+"\\AppleWWDRCA.cer"));


            var b = sigener.Sign(TEST, pass, apple);

        }
    }
}
