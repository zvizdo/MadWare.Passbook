using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.Fields
{
    public class StandardField : Field<string>
    {
        public StandardField(string key, string label, string value) : base(key, label, value)
        {
        }

        public override void SetValue(string value)
        {
            this.Value = value;
        }
    }
}
