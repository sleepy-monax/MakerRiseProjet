using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Storage.MarkdownParser.DataStructure
{

    public enum headSize
    {
        h1,
        h2,
        h3,
        regular
    }

    public class mdLine
    {

        public mdLine(headSize size = headSize.regular) {

        }

    }
}
