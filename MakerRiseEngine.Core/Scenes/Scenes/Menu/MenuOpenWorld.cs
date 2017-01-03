using Maker.RiseEngine.Core.UserInterface.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.Scenes.Scenes.Menu
{
    public class MenuOpenWorld : Scene
    {
        Panel rootContainer;

        public override void OnLoad()
        {

            rootContainer = new Panel(new Rectangle(-350, - (Engine.graphics.PreferredBackBufferHeight / 2), 700, Engine.graphics.PreferredBackBufferHeight), Color.White);
            rootContainer.Padding = new UserInterface.ControlPadding(16);
            rootContainer.ControlAnchor = UserInterface.Anchor.Center;

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            rootContainer.Draw(spriteBatch, gameTime);
        }


        public override void OnUnload()
        {
            
        }

        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            rootContainer.Update(mouse, keyBoard, gameTime);
        }
    }
}
