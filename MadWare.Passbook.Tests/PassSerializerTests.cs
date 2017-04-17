using MadWare.Passbook.Fields;
using MadWare.Passbook.PassSerializer;
using MadWare.Passbook.PassStyle;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MadWare.Passbook.Tests
{
    public class PassSerializerTests
    {

        [Fact]
        public void Basic()
        {
            IPassSerializer s = new JsonPassSerializer();

            var a = new NumberField("int", "Integer", 1, Enums.FieldNumberStyle.PKNumberStyleSpellOut);
            a.DataDetectorTypes = new Enums.DataDetectorType[] { Enums.DataDetectorType.PKDataDetectorTypeAddress, Enums.DataDetectorType.PKDataDetectorTypeLink };
            Pass<GenericPassStyle> p = new Pass<GenericPassStyle>
            {
                TeamIdentifier = "123",
                PassStyle = new GenericPassStyle()
                {
                    HeaderFields = new List<Fields.Field> { a }
                }
            };

            byte[] passJsonBytes = s.Serialize(p);
        }

    }
}
