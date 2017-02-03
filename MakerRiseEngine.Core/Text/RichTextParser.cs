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
                string text = tags[1];
                tags = tags[0].Split(';');

                string colorTag = tags[0];

                // Parse color.
                var colorRGB = colorTag.Split(',');

                int ColorR;
                int ColorG;
                int ColorB;

                int.TryParse(colorRGB[0], out ColorR);
                int.TryParse(colorRGB[1], out ColorG);
                int.TryParse(colorRGB[2], out ColorB);

                // Parse 
                string styleTag = tags[1];

                //TODO : finish code here !
                
            }

            return Blocks;
        }

    }
}
