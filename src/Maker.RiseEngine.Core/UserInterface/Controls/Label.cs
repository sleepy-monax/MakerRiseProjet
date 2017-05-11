using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Core.UserInterface.Controls
{
    public class Label : Control
    {

        public Rendering.Alignment TextAlignment { get; set; } = Rendering.Alignment.Center;
        public Rendering.Style TextStyle { get; set; } = Rendering.Style.DropShadow;

        public Label(string text, Rectangle rect, Color textColor) {
            Text = text;
            ControlRectangle = rect;
            TextColor = textColor;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawText(spriteBatch, TextFont, Text, new Rectangle(0,0,ControlRectangle.Width, ControlRectangle.Height), TextColor, TextAlignment, TextStyle);
        }

    }
}
