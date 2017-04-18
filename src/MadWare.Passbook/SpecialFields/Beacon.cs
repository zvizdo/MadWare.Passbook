using System;
using System.Collections.Generic;
using System.Text;

namespace MadWare.Passbook.SpecialFields
{
    public class Beacon
    {
        /// <summary>
        /// Required. Unique identifier of a Bluetooth Low Energy location beacon.
        /// </summary>
        public string ProximityUUID { get; set; }

        /// <summary>
        /// Optional. Text displayed on the lock screen when the pass is currently relevant.
        /// </summary>
        public string RelevantText { get; set; }

        public int? Major { get; set; }

        public int? Minor { get; set; }

        public Beacon(string proximityUUID, string relevantText = null, int? major = null, int? minor = null)
        {
            this.ProximityUUID = proximityUUID;
            this.RelevantText = relevantText;
            this.Major = major;
            this.Minor = minor;
        }
    }
}
