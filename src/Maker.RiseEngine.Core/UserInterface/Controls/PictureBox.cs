using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.UserInterface.Controls
{
    public class PictureBox : Control
    {
        public Texture2D Picture { get; set; }


        public PictureBox(Texture2D picture, Rectangle rect, Color color)
        {
            Picture = picture;
            Bound = rect;
            ControlColor = color;
        }

        public PictureBox(System.Drawing.Bitmap picture, Rectangle rect, Color color)
        {
            Picture = Rendering.BitmapHelper.BitmapToTexture2D(Rise.Engine.GraphicsDevice, picture);
            Bound = rect;
            ControlColor = color;
        }


        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawTexture2D(spriteBatch, Picture, new Rectangle(Bound.Width / 2 - Picture.Width / 2, Bound.Height / 2 - Picture.Height / 2, Picture.Width, Picture.Height), ControlColor);
        }

    }
}
