using System;
using System.Collections.Generic;
using System.Text;

namespace MadWare.Passbook.SpecialFields
{
    public class Location
    {
        /// <summary>
        /// Optional. Altitude, in meters, of the location.
        /// </summary>
        public double? Altitude { get; set; }

        /// <summary>
        /// Required. Latitude, in degrees, of the location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Required. Longitude, in degrees, of the location.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Optional. Text displayed on the lock screen when the pass is currently relevant.
        /// </summary>
        public string RelevantText { get; set; }

        public Location() { }

        public Location(double lat, double lon, string relevantText = null, double? alt = null)
        {
            this.Latitude = lat;
            this.Longitude = lon;
            this.RelevantText = relevantText;
            this.Altitude = alt;
        }
    }
}
