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

            var a = new IntegerField("int", "Integer", 1, Enums.FieldNumberStyle.PKNumberStyleSpellOut);
            a.DataDetectorTypes = new Enums.DataDetectorType[] { Enums.DataDetectorType.PKDataDetectorTypeAddress, Enums.DataDetectorType.PKDataDetectorTypeLink };
            var p = new Pass<BoardingPassStyle>
            {
                TeamIdentifier = "123",
                PassStyle = new BoardingPassStyle(Enums.TransitType.PKTransitTypeAir)
                {
                    HeaderFields = new List<Fields.Field> { a }
                },
                Locations = new List<SpecialFields.Location> { new SpecialFields.Location(1, 2, relevantText: "abc") },
                Localizations = new Dictionary<string, SpecialFields.Localization> {  }
            };

            byte[] passJsonBytes = s.Serialize(p);
        }

    }
}
