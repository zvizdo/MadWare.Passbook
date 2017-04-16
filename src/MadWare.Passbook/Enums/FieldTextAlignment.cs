using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.Enums
{
    /// <summary>
    /// Won't be used in primary fields.
    /// </summary>
    public enum FieldTextAlignment
    {
        Unspecified,
        /// <summary>
        /// Left align text
        /// </summary>
        PKTextAlignmentLeft,
        /// <summary>
        /// Center align text
        /// </summary>
        PKTextAlignmentCenter,
        /// <summary>
        /// Right align text
        /// </summary>
        PKTextAlignmentRight,
        /// <summary>
        /// Natural alignment, aligns the text appropriately based on its script direction.
        /// </summary>
        PKTextAlignmentNatural
    }
}
