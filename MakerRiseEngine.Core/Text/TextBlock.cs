using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Maker.RiseEngine.Core.Rendering;

namespace Maker.RiseEngine.Core.Text
{
    public class TextBlock
    {

        public string Text;
        public Color Color;
        public Style Style;

        public TextBlock(string text, Color color, Style style) {
            Text = text;
            Color = color;
            Style = style;
        }
    }
}
