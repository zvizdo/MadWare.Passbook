using MadWare.Passbook.PassStyle;
using System;
using System.Collections.Generic;

namespace MadWare.Passbook
{
    public class Pass<T> where T : BasePassStyle
    {
        #region Standard Keys

        /// <summary>
        /// Required. Pass type identifier, as issued by Apple. The value must correspond with your signing certificate.
        /// </summary>
        public string PassTypeIdentifier { get; set; }

        /// <summary>
        /// Required. Version of the file format. The value must be 1.
        /// </summary>
        public int FormatVersion { get { return 1; } }

        /// <summary>
        /// Required. Serial number that uniquely identifies the pass. No two passes with the same pass type identifier may have the same serial number.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Required. A simple description of the pass
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Required. Team identifier of the organization that originated and signed the pass, as issued by Apple.
        /// </summary>
        public string TeamIdentifier { get; set; }

        /// <summary>
        /// Required. Display name of the organization that originated and signed the pass.
        /// </summary>
        public string OrganizationName { get; set; }

        #endregion Standard Keys

        #region Expiration Keys

        public DateTime? ExpirationDate { get; set; }

        public Boolean? Voided { get; set; }

        #endregion Expiration Keys

        #region Visual Appearance Keys

        /// <summary>
        /// Optional. Foreground color of the pass, specified as a CSS-style RGB triple. For example, rgb(100, 10, 110).
        /// </summary>
        public string ForegroundColor { get; set; }

        /// <summary>
        /// Optional. Background color of the pass, specified as an CSS-style RGB triple. For example, rgb(23, 187, 82).
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Optional. Color of the label text, specified as a CSS-style RGB triple. For example, rgb(255, 255, 255).
        /// If omitted, the label color is determined automatically.
        /// </summary>
        public string LabelColor { get; set; }

        /// <summary>
        /// Optional. Text displayed next to the logo on the pass.
        /// </summary>
        public string LogoText { get; set; }

        /// <summary>
        /// Optional. If true, the strip image is displayed without a shine effect. The default value is false.
        /// </summary>
        public bool? SuppressStripShine { get; set; }

        /// <summary>
        /// Optional for event tickets and boarding passes; otherwise not allowed. Identifier used to group related passes
        /// </summary>
        public string GroupingIdentifier { get; set; }

        /// <summary>
        /// Pass style information, like header fields, secondary fields,...
        /// </summary>
        public T PassStyle { get; set; }

        #endregion Visual Appearance Keys

        #region Relevance Keys

        /// <summary>
        /// Optional. Always display the time and date in the given time zone, not in the user’s current time zone. The default value is false
        /// </summary>
        public Boolean? IgnoresTimeZone { get; set; }

        /// <summary>
        /// Optional. Date and time when the pass becomes relevant. For example, the start time of a movie.
        /// </summary>
        public DateTime? RelevantDate { get; set; }

        /// <summary>
        /// Optional. Locations where the passisrelevant. For example, the location of your store.
        /// </summary>
        //public List<RelevantLocation> RelevantLocations { get; private set; }

        /// <summary>
        /// Optional. Beacons marking locations where the pass is relevant.
        /// </summary>
        //public List<RelevantBeacon> RelevantBeacons { get; private set; }

        /// <summary>
        /// Optional. Maximum distance in meters from a relevant latitude and longitude that the pass is relevant
        /// </summary>
        public int? MaxDistance { get; set; }

        #endregion Relevance Keys

        #region Web Service Keys

        /// <summary>
        /// The authentication token to use with the web service.
        /// </summary>
        public string AuthenticationToken { get; set; }

        /// <summary>
        /// The URL of a web service that conforms to the API described in Pass Kit Web Service Reference.
        /// The web service must use the HTTPS protocol and includes the leading https://.
        /// On devices configured for development, there is UI in Settings to allow HTTP web services.
        /// </summary>
        public string WebServiceUrl { get; set; }

        #endregion Web Service Keys

        #region Associated App Keys

        public List<int> AssociatedStoreIdentifiers { get; set; }

        public string AppLaunchURL { get; set; }

        #endregion Associated App Keys

        #region User Info Keys

        public Object UserInfo { get; set; }

        #endregion User Info Keys
    }
}