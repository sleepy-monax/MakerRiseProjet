using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiseEngine.Core.UI.Controls
{
    class Label : Control
    {

        string Text;
        Color FontColor;
        helper.Alignment Align;
        helper.Style Style;

        public Label(string text, int Width, int x, int y, helper.Alignment align, helper.Style style, Color fontColor)
        {

            SizeBox.Width = Width;
            SizeBox.Height = 64;
            SizeBox.X = x;
            SizeBox.Y = y;

            Text = text;
            FontColor = fontColor;
            Align = align;

            Style = style;

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {

            spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, this.ClickRect, Align, Style, FontColor);
            base.Draw(spriteBatch, gameTime, x, y);
        }

    }
}
