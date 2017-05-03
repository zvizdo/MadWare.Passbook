using MadWare.Passbook.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MadWare.Passbook.PassStyle
{
  
    public class BoardingPassStyle : BasePassStyle
    {
        /// <summary>
        /// Required for boarding passes; otherwise not allowed. Type of transit.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TransitType TransitType { get; set; }

        public BoardingPassStyle() : this(TransitType.PKTransitTypeAir) { }

        public BoardingPassStyle(TransitType transitType) : base()
        {
            this.TransitType = transitType;
        }
    }
}
