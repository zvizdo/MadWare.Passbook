using MadWare.Passbook.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadWare.Passbook.SpecialFields
{
    public class Barcode
    {
        /// <summary>
        /// Required. Barcode format
        /// </summary>
        public BarcodeType Type { get; set; }

        /// <summary>
        /// Required. Message or payload to be displayed as a barcode.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Required. Text encoding that is used to convert the message from the string representation to a data representation to render the barcode. The value is typically iso-8859-1, but you may use another encoding that is supported by your barcode scanning infrastructure.
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// Optional. Text displayed near the barcode. For example, a human-readable version of the barcode data in case the barcode doesn’t scan.
        /// </summary>
        public string AlternateText { get; set; }

        public Barcode(BarcodeType type, string message, string encoding)
        {
            Type = type;
            Message = message;
            Encoding = encoding;
        }

        public Barcode(BarcodeType type, string message, string encoding, string alternateText)
        {
            Type = type;
            Message = message;
            Encoding = encoding;
            AlternateText = alternateText;
        }
    }
}
