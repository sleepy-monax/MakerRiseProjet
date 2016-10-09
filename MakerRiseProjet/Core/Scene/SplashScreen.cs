using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RiseEngine.Core.UI;

namespace RiseEngine.Core.Scene
{
    public class SplashScreen : BaseObject
    {
        bool OneTime = true;
        bool FirstFrame = true;

        bool EngineLogo = false;

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
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




            base.Update(Mouse, KeyBoard, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (EngineLogo)
            {
                spriteBatch.Draw(ContentEngine.Texture2D("Engine", "MakerLogo"), new Rectangle(Common.graphics.PreferredBackBufferWidth / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Width / 2, Common.graphics.PreferredBackBufferHeight / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Height / 2, ContentEngine.Texture2D("Engine", "MakerLogo").Width, ContentEngine.Texture2D("Engine", "MakerLogo").Height), Color.White);
            }
            else
            {
                spriteBatch.Draw(ContentEngine.Texture2D("Engine", "MonoGameLogo"), new Rectangle(Common.graphics.PreferredBackBufferWidth / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Width / 2, Common.graphics.PreferredBackBufferHeight / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Height / 2, ContentEngine.Texture2D("Engine", "MakerLogo").Width, ContentEngine.Texture2D("Engine", "MakerLogo").Height), Color.White);
            }

            if (FirstFrame)
            {
                
                spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), "Loading data...", new Rectangle(0, Common.graphics.PreferredBackBufferHeight - 64, Common.graphics.PreferredBackBufferWidth, 64), helper.Alignment.Center, helper.Style.DropShadow, Color.Black);

            }


            FirstFrame = false;

            



            base.Draw(spriteBatch, gameTime);
        }

    }
}
