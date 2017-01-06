using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using System;

namespace Maker.twiyol.Scenes
{
    class WorldGenerating : Scene
    {

        public string message = "Génération du terrain...";
        public int Progress = 0;

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SpriteFontDraw.DrawString(spriteBatch, ContentEngine.SpriteFont("Engine", "Bebas_Neue_48pt"), message, new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Alignment.Center, Style.rectangle, Color.White);
            SpriteFontDraw.DrawString(spriteBatch, ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Progress + "%", new Rectangle(0, 128, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Alignment.Center, Style.DropShadow, Color.White);
        }

        public override void OnLoad()
        {

        }

        public override void OnUnload()
        {

        }

        public override void OnUpdate(PlayerInput playerInput, GameTime gameTime)
        {
            
        }


    }
}
