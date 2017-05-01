using MadWare.Passbook.Extensions;
using MadWare.Passbook.Fields;
using MadWare.Passbook.PassStyle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MadWare.Passbook.Tests
{
    public class PassValidationTests
    {


        public static IEnumerable<object[]> TestPass()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory; //Assembly.GetExecutingAssembly().Location;
            var cwd = new DirectoryInfo(path).Parent.Parent.Parent;
            path = cwd.FullName;

            return new[] {
                new object[] {
                    new  Pass<StoreCardPassStyle> {
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
                    Images = null,
                    },
                    true
                },
                new object[] {
                    new  Pass<StoreCardPassStyle> {
                         SerialNumber = "123456789",
                         Description = "test",
                         OrganizationName = "Test",
                         BackgroundColor = Utils.ToColor(101,51,113),
                         LabelColor = Utils.ToColor(101,51,113),
                         ForegroundColor = Utils.ToColor(255,255,255),
                         PassStyle = new StoreCardPassStyle
                         {
                         PrimaryFields = new List<Field>
                         {
                            new StandardField("name", "Anže", "Kravanja")
                         }

                },
                    Images = new Dictionary<Enums.PassbookImageType, byte[]>{ { Enums.PassbookImageType.Icon, null }, { Enums.PassbookImageType.IconRetina, null }  },
                    },
                    false
                },
                  new object[] {
                    new  Pass<StoreCardPassStyle> {
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
                    Images = new Dictionary<Enums.PassbookImageType, byte[]>{ { Enums.PassbookImageType.Icon, null } },
                    },
                    true
                },
                  new object[] {
                    new  Pass<StoreCardPassStyle> {
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
                    Images = new Dictionary<Enums.PassbookImageType, byte[]>{ { Enums.PassbookImageType.IconRetina, null } },
                    },
                    true
                }

            };
        }

        [Theory]
        [MemberData("TestPass")]
        public void TestPassValidation<T>(Pass<T> p, bool throwEx) where T : BasePassStyle
        {
            string path = AppDomain.CurrentDomain.BaseDirectory; //Assembly.GetExecutingAssembly().Location;
            var cwd = new DirectoryInfo(path).Parent.Parent.Parent;
            path = cwd.FullName;

            var pass = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\pass.com.eon4u.p12"), "eon4u");
            var apple = Helpers.LoadCertificateFromBytes(File.ReadAllBytes(path + "\\resources\\AppleWWDRCA.cer"));
            var opt = new PassGeneratorOptions { PassCert = pass, AppleCert = apple };

            var passGenerator = new PassGenerator(opt);
            try
            {
                passGenerator.ValidatePass(p);
                Xunit.Assert.False(throwEx);
            }
            catch (Exception ex)
            {
                Xunit.Assert.True(throwEx);
            }
        }
    }
}
