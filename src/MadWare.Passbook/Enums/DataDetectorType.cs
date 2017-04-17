using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MadWare.Passbook.Enums
{
    public enum DataDetectorType
    {
        /// <summary>
        /// Automatically detect phone numbers
        /// </summary>
        PKDataDetectorTypePhoneNumber,
        /// <summary>
        /// Automatically detect links
        /// </summary>
        PKDataDetectorTypeLink,
        /// <summary>
        /// Automatically detect addresses
        /// </summary>
        PKDataDetectorTypeAddress,
        /// <summary>
        /// Automatically detect calendar events
        /// </summary>
        PKDataDetectorTypeCalendarEvent
    }
}
