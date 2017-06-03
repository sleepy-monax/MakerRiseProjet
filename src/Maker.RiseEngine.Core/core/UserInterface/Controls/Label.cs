using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Core.UserInterface.Controls
{
    public class Label : Control
    {

        public Alignment TextAlignment { get; set; } = Alignment.Center;
        public Style TextStyle { get; set; } = Style.DropShadow;

        public Label(string text, Rectangle rect, Color textColor) {
            Text = text;
            Bound = rect;
            TextColor = textColor;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawText(spriteBatch, TextFont, Text, new Rectangle(0, 0, Bound.Width, Bound.Height), TextColor, TextAlignment, TextStyle);
        }

    }
}
