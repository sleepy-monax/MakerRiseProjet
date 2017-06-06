using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Ressources
{
    static class Helper
    {

        public static string ToDosLineEnd(this string str)
        {
            // Convert LF to CRLF.
            var newString = str.Replace("\r", "").Replace("\n", "\r\n");

            return newString;
        }

    }
}
