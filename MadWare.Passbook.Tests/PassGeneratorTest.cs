using MadWare.Passbook.Fields;
using MadWare.Passbook.PassSerializer;
using MadWare.Passbook.PassSigner;
using MadWare.Passbook.PassStyle;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MadWare.Passbook.Tests
{
    public class PassGeneratorTest
    {
        public class PassGeneratorMock : PassGenerator
        {
            public PassGeneratorMock(PassGeneratorOptions opt, IPassSerializer passSerializer, IPassSigner passSigner) : base(opt, passSerializer, passSigner)
            {
            }
        }

        [Fact]
        public void Basic()
        {
            var p = new Pass<GenericPassStyle>
            {
                PassStyle = new GenericPassStyle
                {
                    HeaderFields = new List<Field>
                        {
                            new StandardField("name", "Anže", "Kravanja")
                        }
                }
            };

            string path = AppDomain.CurrentDomain.BaseDirectory; //Assembly.GetExecutingAssembly().Location;
            var cwd = new DirectoryInfo(path).Parent.Parent.Parent;
            path = cwd.FullName;

            var pass = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\pass.com.eon4u.p12"), "eon4u");
            var apple = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\AppleWWDRCA.cer"));
            var opt = new PassGeneratorOptions { PassCert = pass, AppleCert = apple };

            var passGenerator = new PassGenerator(opt);
            byte[] pkPass = passGenerator.Generate(p);
            File.WriteAllBytes(path + "\\resources\\pass.pkpass", pkPass);

        }


    }
}
