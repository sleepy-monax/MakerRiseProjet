using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.Scene
{
    public class WorldGenerating : Idrawable
    {

        public string message = "";

        public WorldGenerating() {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SceneManager.MainMn.BackgroundSB.Begin();
            SceneManager.MainMn.Background.Draw(SceneManager.MainMn.BackgroundSB, gameTime);
            SceneManager.MainMn.BackgroundSB.End();

            spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), message, new Rectangle(0, 0, Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight), Alignment.Center, Style.DropShadow, Color.White);

        }

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            SceneManager.MainMn.Background.Update(Mouse, KeyBoard, gameTime);
        }
    }
}
