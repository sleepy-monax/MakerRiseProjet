using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using System;

namespace Maker.Twiyol.Scenes
{
    class WorldGenerating : Scene
    {

        public string message = "Génération du terrain...";
        public int Progress = 0;

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SpriteFontDraw.DrawString(spriteBatch, Engine.ressourceManager.GetSpriteFont("Engine", "Bebas_Neue_48pt"), message, new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), Alignment.Center, Style.rectangle, Color.White);
            SpriteFontDraw.DrawString(spriteBatch, Engine.ressourceManager.GetSpriteFont("Engine", "segoeUI_16pt"), Progress + "%", new Rectangle(0, 128, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), Alignment.Center, Style.DropShadow, Color.White);
        }

        public override void OnLoad()
        {

        }

        public override void OnUnload()
        {

        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            
        }


    }
}
