using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Core.UserInterface.Controls
{
    public class PictureBox : Control
    {
        public Texture2D Picture { get; set; }


        public PictureBox(Texture2D picture, Rectangle rect, Color color)
        {

            Picture = picture;
            ControlRectangle = rect;
            ControlColor = color;

        }

        public PictureBox(System.Drawing.Bitmap picture, Rectangle rect, Color color)
        {

            Picture = Rendering.BitmapHelper.BitmapToTexture2D(Engine.GraphicsDevice, picture);
            ControlRectangle = rect;
            ControlColor = color;

        }


        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawT2D(spriteBatch, Picture, new Rectangle(ControlRectangle.Width / 2 - Picture.Width / 2, ControlRectangle.Height / 2 - Picture.Height / 2, Picture.Width, Picture.Height), ControlColor);
        }

    }
}
