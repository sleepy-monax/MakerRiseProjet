using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Maker.RiseEngine.Core.Content;

namespace Maker.RiseEngine.Core.SceneManager.Scenes
{
    class WorldGenerating : Scene
    {

        public string message = "Génération du terrain...";
        public int Progress = 0;

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rendering.SpriteFontDraw.DrawString(spriteBatch, ContentEngine.SpriteFont("Engine", "Bebas_Neue_48pt"), message, new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Rendering.SpriteFontDraw.Alignment.Center, Rendering.SpriteFontDraw.Style.rectangle, Color.White);
            Rendering.SpriteFontDraw.DrawString(spriteBatch, ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Progress + "%", new Rectangle(0, 128, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Rendering.SpriteFontDraw.Alignment.Center, Rendering.SpriteFontDraw.Style.DropShadow, Color.White);
        }

        public override void OnLoad()
        {

        }

        public override void OnUnload()
        {

        }

        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {

        }
    }
}
