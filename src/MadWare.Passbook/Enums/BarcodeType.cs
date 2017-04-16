using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.Enums
{
    /// <summary>
    /// Barcode format
    /// </summary>
    public enum BarcodeType
    {
        /// <summary>
        /// QRCode
        /// </summary>
        PKBarcodeFormatQR = 1,
        /// <summary>
        /// PDF-417
        /// </summary>
        PKBarcodeFormatPDF417,
        /// <summary>
        /// Aztec
        /// </summary>
        PKBarcodeFormatAztec,
        /// <summary>
        /// Code128
        /// </summary>
        PKBarcodeFormatCode128,
    }
}
