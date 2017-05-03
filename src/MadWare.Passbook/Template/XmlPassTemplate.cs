using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadWare.Passbook.PassStyle;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace MadWare.Passbook.Template
{
    public class XmlPassTemplate : IPassTemplate
    {
        public Pass<T> ReadTemplate<T>(string template) where T : BasePassStyle
        {
            XmlSerializer xSer = new XmlSerializer(typeof(Pass<T>));
            using (StringReader sr = new StringReader(template))
            {
                var pass = xSer.Deserialize(sr);
                return (Pass<T>)pass;
            }
        }

        public string SaveTemplate<T>(Pass<T> p) where T : BasePassStyle
        {
            if (p == null)
            {
                return String.Empty;
            }

            XmlSerializer serelizer = new XmlSerializer(p.GetType());
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                serelizer.Serialize(writer, p);
                return stringWriter.ToString();
            }
        }
    }
}
