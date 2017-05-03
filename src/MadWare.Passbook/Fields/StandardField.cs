using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MadWare.Passbook.Fields
{
    public class StandardField : Field<string>
    {
        public StandardField() : base() { }

        public StandardField(string key, string label, string value) : base(key, label, value)
        {
        }

        public override void SetValue(string value)
        {
            this.Value = value;
        }
    }
}
