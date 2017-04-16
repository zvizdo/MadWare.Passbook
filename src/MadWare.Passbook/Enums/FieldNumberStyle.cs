using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.Enums
{
    public enum FieldNumberStyle
    {
        PKNumberStyleDecimal,
        /// <summary>
        /// Specifies a currency style format.
        /// </summary>
        PKNumberStylePercent,
        /// <summary>
        /// Specifies a percent style format.
        /// </summary>
        PKNumberStyleScientific,
        /// <summary>
        /// Specifies a spell-out format; for example, "23" becomes "twenty-three".
        /// </summary>
        PKNumberStyleSpellOut
    }
}
