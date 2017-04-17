using MadWare.Passbook.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.Fields
{
    public class DateTimeField : Field<DateTime>
    {
        public DateTimeField(string key, string label, DateTime value) : base(key, label, value)
        {
        }

        public DateTimeField(string key, string label, DateTime value, FieldDateTimeStyle dateStyle, FieldDateTimeStyle timeStyle) : base(key, label, value)
        {
            this.DateStyle = dateStyle;
            this.TimeStyle = timeStyle;
        }

        /// <summary>
		/// Style of date to display, must be a <see cref="FieldDateTimeStyle" />
		/// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldDateTimeStyle DateStyle { get; set; }

        /// <summary>
        /// Style of time to display, must be a <see cref="FieldDateTimeStyle" />
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldDateTimeStyle TimeStyle { get; set; }

        /// <summary>
		/// <para>Optional. If true, the label's value is displayed as a relative date; otherwise, it is displayed as an absolute date. The default value is false.</para>
		/// <para>This does not affect how relevance is calculated.</para>
		/// </summary>
        public bool? IsRelative { get; set; }

        /// <summary>
        /// <para>Optional. Always display the time and date in the given time zone, not in the user's current time zone. The default value is false.</para>
        /// <para>The format for a date and time always requires a time zone, even if it will be ignored. For backward compatibility with iOS 6, provide an appropriate time zone, so that the information is displayed meaningfully even without ignoring time zones.</para>
        /// <para>This key does not affect how relevance is calculated.</para>
        /// <para>Available in iOS 7.0.</para>
        /// </summary>
        public bool? IgnoresTimeZone { get; set; }
    }
}