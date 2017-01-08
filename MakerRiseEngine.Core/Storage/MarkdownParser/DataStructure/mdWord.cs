using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Storage.MarkdownParser.DataStructure
{
    public enum mdWordStyle {
        Italic,
        Bold,
        Underline,
        Strikeout,
        none
    }

    public class mdWord
    {
        public mdWord(string text, mdWordStyle style = mdWordStyle.none) {


        }

    }
}
