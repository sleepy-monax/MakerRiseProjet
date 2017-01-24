using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.UserInterface;
using Maker.RiseEngine.Core.UserInterface.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.twiyol.Scenes.Menu
{
    public class MenuOpenWorld : Scene
    {
        Panel rootContainer;

        public override void OnLoad()
        {

            rootContainer = new Panel(new Rectangle(-350, -(Engine.graphics.PreferredBackBufferHeight / 2), 700, Engine.graphics.PreferredBackBufferHeight), Color.White);
            rootContainer.Padding = new ControlPadding(16);
            rootContainer.ControlAnchor = Anchor.Center;

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            rootContainer.Draw(spriteBatch, gameTime);
        }


        public override void OnUnload()
        {

        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            rootContainer.Update(playerInput, gameTime);
        }
    }
}
