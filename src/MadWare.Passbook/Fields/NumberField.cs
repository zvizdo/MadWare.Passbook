using MadWare.Passbook.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.Fields
{
    public class IntegerField : Field<int>
    {
        public IntegerField(string key, string label, int value) : base(key, label, value)
        {
        }

        public override void SetValue(int value)
        {
            this.Value = value.ToString();
        }
    }

    public class NumberField : Field<double>
    {
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
        [JsonProperty(PropertyName = "currencyCode")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Style of number to display. Must be one of <see cref="FieldNumberStyle" />
        /// </summary>
        [JsonProperty(PropertyName = "numberStyle")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldNumberStyle? NumberStyle { get; set; }

    }
}
