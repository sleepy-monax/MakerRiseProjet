using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.SceneManager.Scenes
{
    public class EngineLoading : Scene
    {

        public string Message = "Loading...";

        public override void OnLoad()
        {
            this.show();
            ThreadStart GenHandle = new ThreadStart(delegate
            {
                Engine.Initialize(this);

                Scene menu = new Menu.MenuMain();
                Game.sceneManager.AddScene(menu);
                menu.show();
                Game.sceneManager.RemoveScene(this);
            });
            Thread t = new Thread(GenHandle);
            t.Start();
        }

        public override void OnUnload()
        {

        }

        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(ContentEngine.Texture2D("Engine", "MakerLogo"), new Rectangle(Engine.graphics.PreferredBackBufferWidth / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Width / 2, Engine.graphics.PreferredBackBufferHeight / 2 - ContentEngine.Texture2D("Engine", "MakerLogo").Height / 2, ContentEngine.Texture2D("Engine", "MakerLogo").Width, ContentEngine.Texture2D("Engine", "MakerLogo").Height), Color.White);
            spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), Message, new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Alignment.Top, Style.DropShadow, Color.Black);
        }
    }
}
