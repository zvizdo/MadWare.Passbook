using MadWare.Passbook.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MadWare.Passbook.Fields
{
    public class NumberField : Field<double>
    {
        public NumberField() : base() { }

        public NumberField(string key, string label, double value) : base(key, label, value)
        {
        }

        public NumberField(string key, string label, double value, FieldNumberStyle numberStyle) : this(key, label, value)
        {
            this.NumberStyle = numberStyle;
        }

        /// <summary>
		/// ISO 4217 currency code for the field’s value.
		/// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Style of number to display. Must be one of <see cref="FieldNumberStyle" />
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldNumberStyle? NumberStyle { get; set; }

    }

    public class IntegerField : NumberField
    {
        public IntegerField(string key, string label, int value) : base(key, label, value)
        {
        }

        public IntegerField(string key, string label, int value, FieldNumberStyle numberStyle) : base(key, label, value, numberStyle)
        {
        }

        public override void SetValue(double value)
        {
            this.Value = ((int)value).ToString();
        }
    }
}
