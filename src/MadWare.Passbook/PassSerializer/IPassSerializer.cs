using MadWare.Passbook.PassStyle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadWare.Passbook.PassSerializer
{
    public interface IPassSerializer
    {
        byte[] Serialize<T>(Pass<T> p) where T : BasePassStyle;
    }
}
