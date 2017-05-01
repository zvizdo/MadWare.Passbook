using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadWare.Passbook.Extensions
{
    public class Utils
    {
        public static string ToColor(int r, int g, int b)
        {
            return String.Format("rgb({0},{1},{2})", r, g, b);
        }

    }
}
