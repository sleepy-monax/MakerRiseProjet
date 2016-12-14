using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.Scene
{
    public class SplashScreen : Idrawable
    {
        bool OneTime = true;
        bool FirstFrame = true;

        bool EngineLogo = false;

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            if (OneTime == true)
            {
                if (FirstFrame == false)
                {
                    Engine.Initializer();

                    OneTime = false;

                }
            }

            if (gameTime.TotalGameTime.Seconds > 1)
            {
                EngineLogo = true;
            }

            if (gameTime.TotalGameTime.Seconds > 2)
            {
                Core.Scene.SceneManager.CurrentScene = 0;
            }




        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (EngineLogo)
            {
                spriteBatch.Draw(ContentEngine.Texture2D("Engine", "MakerLogo"), new Rectangle(Engine.graphics.PreferredBackBufferWidth / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Width / 2, Engine.graphics.PreferredBackBufferHeight / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Height / 2, ContentEngine.Texture2D("Engine", "MakerLogo").Width, ContentEngine.Texture2D("Engine", "MakerLogo").Height), Color.White);
            }
            else
            {
                spriteBatch.Draw(ContentEngine.Texture2D("Engine", "MonoGameLogo"), new Rectangle(Engine.graphics.PreferredBackBufferWidth / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Width / 2, Engine.graphics.PreferredBackBufferHeight / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Height / 2, ContentEngine.Texture2D("Engine", "MakerLogo").Width, ContentEngine.Texture2D("Engine", "MakerLogo").Height), Color.White);
            }

            if (FirstFrame)
            {

                spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), "Loading data...", new Rectangle(0, Engine.graphics.PreferredBackBufferHeight - 64, Engine.graphics.PreferredBackBufferWidth, 64), Alignment.Center, Style.DropShadow, Color.Black);

            }


            FirstFrame = false;

        }

    }
}
