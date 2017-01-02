using Maker.RiseEngine.Core.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.UI.Controls
{
    class Label : Control
    {

        string Text;
        Color FontColor;
        Alignment Align;
        Style Style;

        public Label(string text, int Width, int x, int y, Alignment align, Style style, Color fontColor)
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
