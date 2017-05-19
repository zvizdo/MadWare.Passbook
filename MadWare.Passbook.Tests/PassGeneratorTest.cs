using MadWare.Passbook.Enums;
using MadWare.Passbook.Fields;
using MadWare.Passbook.PassSerializer;
using MadWare.Passbook.PassSigner;
using MadWare.Passbook.PassStyle;
using MadWare.Passbook.SpecialFields;
using MadWare.Passbook.Template;
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
            string path = AppDomain.CurrentDomain.BaseDirectory; //Assembly.GetExecutingAssembly().Location;
            var cwd = new DirectoryInfo(path).Parent.Parent.Parent;
            path = cwd.FullName;

            var images = new SerializableDictionary<Enums.PassbookImageType, byte[]>();
            images.Add(Enums.PassbookImageType.Icon, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.IconRetina, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.LogoRetina, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.Logo, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));

            var p = new Pass<StoreCardPassStyle>
            {
                SerialNumber = "123456789",
                Description = "test",
                OrganizationName = "Test",
                BackgroundColor = "rgb(101,51,113)",
                LabelColor = "rgb(255,255,255)",
                ForegroundColor = "rgb(255,255,255)",
                PassStyle = new StoreCardPassStyle
                {

                    PrimaryFields = new List<Field>
                        {
                            new StandardField("name", "Anže", "Kravanja")
                        }

                },
                Images = images,
            };


            var pass = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\pass.com.eon4u.p12"), "eon4u");
            var apple = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\AppleWWDRCA.cer"));
            var opt = new PassGeneratorOptions { PassCert = pass, AppleCert = apple };

            var passGenerator = new PassGenerator(opt);
            byte[] pkPass = passGenerator.Generate(p);
            File.WriteAllBytes(path + "\\resources\\pass.pkpass", pkPass);

        }

        [Fact]
        public void SecondBasic()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory; //Assembly.GetExecutingAssembly().Location;
            var cwd = new DirectoryInfo(path).Parent.Parent.Parent;
            path = cwd.FullName;

            var images = new SerializableDictionary<Enums.PassbookImageType, byte[]>();
            images.Add(Enums.PassbookImageType.Icon, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.IconRetina, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.LogoRetina, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.Logo, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));

            var p = new Pass<StoreCardPassStyle>
            {
                SerialNumber = "123456789",
                Description = "test",
                OrganizationName = "Test",
                BackgroundColor = "rgb(102,153,51)",
                LabelColor = "rgb(255,255,255)",
                ForegroundColor = "rgb(255,255,255)",
                PassStyle = new StoreCardPassStyle
                {

                    PrimaryFields = new List<Field>
                        {
                            new StandardField("name", "A", "K")
                        },
                    Barcode = new Barcode(BarcodeType.PKBarcodeFormatQR, "C03FC299-9D27-47EC-A30C-FA8A0A595C09", "ISO-8859-1", "TO JE TEXT OD SPODAJ"),

                },
                Images = images,

            };


            var pass = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\pass.com.eon4u.p12"), "eon4u");
            var apple = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\AppleWWDRCA.cer"));
            var opt = new PassGeneratorOptions { PassCert = pass, AppleCert = apple };

            var passGenerator = new PassGenerator(opt);
            byte[] pkPass = passGenerator.Generate(p);
            File.WriteAllBytes(path + "\\resources\\pass.pkpass", pkPass);

        }


    }
}
