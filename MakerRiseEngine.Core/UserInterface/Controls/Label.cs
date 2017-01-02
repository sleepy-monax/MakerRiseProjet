using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Core.UserInterface.Controls
{
    public class Label : Control
    {

        public Rendering.SpriteFontDraw.Alignment TextAlignment { get; set; } = Rendering.SpriteFontDraw.Alignment.Center;
        public Rendering.SpriteFontDraw.Style TextStyle { get; set; } = Rendering.SpriteFontDraw.Style.Regular;

        public Label(string text, Rectangle rect, Color color) {
            Text = text;
            ControlRectangle = rect;
            ControlColor = color;
            TextColor = Color.Black;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawText(spriteBatch, ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(0,0,ControlRectangle.Width, ControlRectangle.Height), TextColor, Rendering.SpriteFontDraw.Alignment.Center, Rendering.SpriteFontDraw.Style.DropShadow);
        }

    }
}
