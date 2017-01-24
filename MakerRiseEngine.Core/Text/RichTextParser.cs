using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Text
{
    public static class RichTextParser
    {

        public static List<TextBlock> Parse(string RichText)
        {

            string[] textBlock = RichText.Split('§');
            List<TextBlock> Blocks = new List<TextBlock>();

            foreach (var s in textBlock)
            {

                var tags = s.Split('[');
                tags = tags[0].Split(']');
                tags = tags[0].Split(',');

                string colorTag = tags[0];
                string styleTag = tags[1];

                //TODO : finish code here !

            }

            return Blocks;
        }

    }
}
