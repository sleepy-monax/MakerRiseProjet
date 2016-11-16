using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.Scene
{
    class WorldGenerating : Idrawable
    {

        public WorldGenerating() {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(ContentEngine.Texture2D("Engine", "Loading"), new Rectangle(0,0,Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight), Color.White);
        }

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            
        }
    }
}
