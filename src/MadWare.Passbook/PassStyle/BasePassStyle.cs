using MadWare.Passbook.Fields;
using MadWare.Passbook.SpecialFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MadWare.Passbook.PassStyle
{
    [XmlInclude(typeof(StoreCardPassStyle))]
    [XmlInclude(typeof(GenericPassStyle))]
    [XmlInclude(typeof(BoardingPassStyle))]
    [XmlInclude(typeof(EventTicketPassStyle))]
    [XmlInclude(typeof(CouponPassStyle))]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = true)]
    public abstract class BasePassStyle
    {
        /// <summary>
        /// Optional. Fields to be displayed prominently on the front of the pass.
        /// </summary>
        [XmlArray(Namespace = "HeaderFields", IsNullable = true)]
        public List<Field> HeaderFields { get; set; }

        /// <summary>
        /// Optional. Fields to be displayed prominently on the front of the pass.
        /// </summary>
        public List<Field> PrimaryFields { get; set; }

        /// <summary>
        /// Optional. Fields to be displayed on the front of the pass.
        /// </summary>
        public List<Field> SecondaryFields { get; set; }

        /// <summary>
        /// Optional. Additional fields to be displayed on the front of the pass.
        /// </summary>
        public List<Field> AuxiliaryFields { get; set; }

        /// <summary>
        /// Optional. Information about fields that are displayed on the back of the pass.
        /// </summary>
        public List<Field> BackFields { get; set; }

        public BasePassStyle()
        {
        }

   
    }
}
