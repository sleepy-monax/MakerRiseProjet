using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.UI.Controls
{
    class ProgressBar : Control
    {

        public Color ProgressColor;

        public ProgressBar(int x, int y, int Width, Color progressColor) {

            ProgressColor = progressColor;

        }

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int X, int Y)
        {



            base.Update(Mouse, KeyBoard, gameTime, X, Y);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {



            base.Draw(spriteBatch, gameTime, x, y);
        }

    }
}
