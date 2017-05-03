using MadWare.Passbook.PassStyle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadWare.Passbook.Template
{
    public interface IPassTemplate
    {
        string SaveTemplate<T>(Pass<T> p) where T : BasePassStyle;

        Pass<T> ReadTemplate<T>(string template) where T : BasePassStyle;
    }
}
