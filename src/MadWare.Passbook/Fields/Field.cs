using MadWare.Passbook.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MadWare.Passbook.Fields
{
    /// <summary>
    /// https://developer.apple.com/library/content/documentation/UserExperience/Reference/PassKit_Bundle/Chapters/FieldDictionary.html
    /// </summary>
    public abstract class Field
    {
        /// <summary>
		/// Required. The key must be unique within the scope of the entire pass. For example, “departure-gate”.
		/// </summary>
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// Optional. Label text for the field.
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; protected set; }

        /// <summary>
        /// <para>Optional. Format string for the alert text that is displayed when the pass is updated.</para>
        /// <para>The format string must contain the escape %@, which is replaced with the field's new value.</para>
        /// <para>For example, "Gate changed to %@."</para>
        /// <para>If you don't specify a change message, the user isn't notified when the field changes.</para>
        /// </summary>
        [JsonProperty(PropertyName = "changeMessage")]
        public string ChangeMessage { get; set; }

        /// <summary>
        /// <para>Optional. Alignment for the field’s contents. Must be one of the following values:</para>
        ///	<list type="bullet">
        ///		<item>
        ///			<description>PKTextAlignmentLeft</description>
        ///		</item>
        ///		<item>
        ///			<description>PKTextAlignmentCenter</description>
        ///		</item>
        ///		<item>
        ///			<description>PKTextAlignmentRight</description>
        ///		</item>
        ///		<item>
        ///			<description>PKTextAlignmentNatural</description>
        ///		</item>
        ///	</list>
        /// <para>The default value is natural alignment, which aligns the text appropriately based on its script direction.</para>
        /// <para>This key is not allowed for primary fields or back fields.</para>
        /// </summary>
        [JsonProperty(PropertyName = "textAlignment")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldTextAlignment? TextAlignment { get; set; }

        /// <summary>
		/// <para>Optional. Attributed value of the field.</para>
		/// <para>The value may contain HTML markup for links. Only the &lt;a&gt; tag and its href attribute are supported. For example, the following is key/value pair specifies a link with the text "Edit my profile":</para>
		/// <c>"attributedValue": "&lt;a href='http://example.com/customers/123&gt;>Edit my profile&lt;/a&gt;"</c>
		/// <para>This key's value overrides the text specified by the value key.</para>
		/// <para>Available in iOS 7.0.</para>
		/// </summary>
        [JsonProperty(PropertyName = "attributedValue")]
        public string AttributedValue { get; set; }

        /// <summary>
		/// Optional. Data detectors that are applied to the field’s value. Valid values are:
		///	<list type="bullet">
		///		<item>
		///			<description>PKDataDetectorTypePhoneNumber</description>
		///		</item>
		///		<item>
		///			<description>PKDataDetectorTypeLink</description>
		///		</item>
		///		<item>
		///			<description>PKDataDetectorTypeAddress</description>
		///		</item>
		///		<item>
		///			<description>PKDataDetectorTypeCalendarEvent</description>
		///		</item>
		///	</list>
		/// <para>The default value is all data detectors. Provide an empty array to use no data detectors.</para>
		/// <para>Data detectors are applied only to back fields.</para>
		/// </summary>
        [JsonProperty(PropertyName = "dataDetectorTypes")]
        //[JsonConverter(typeof(StringEnumConverter))]
        public DataDetectorType[] DataDetectorTypes { get; set; }

        public Field(string key, string label)
        {
            this.Key = key;
            this.Label = label;
        }
    }

    public class Field<T> : Field
    {
        public Field(string key, string label, T value) : base(key, label)
        {
            this.SetValue(value);
        }

        public virtual void SetValue(T value)
        {
            this.Value = JsonConvert.SerializeObject(value);
        }
    }
}