using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Maker.RiseEngine.Core.Rendering;

namespace Maker.RiseEngine.Core.SceneManager.Scenes.Menu
{
    public class MenuBackground : Scene
    {

        // BackGround.
        SpriteBatch BackgroundSB;
        Parallax Background;

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, gameTime);
            BackgroundSB.End();
            //spriteBatch.FillRectangle(new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 128));

        }

        public override void OnLoad()
        {

            //Back Ground.
            BackgroundSB = new SpriteBatch(Engine.GraphicsDevice);
            switch (new Random().Next(3))
            {
                case 0:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Dusk Mountain", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));
                    Audio.SongEngine.SwitchSong("Engine", "A Title");
                    break;
                case 1:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Forest", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));
                    Audio.SongEngine.SwitchSong("Engine", "Look Up");
                    break;
                case 2:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));
                    Audio.SongEngine.SwitchSong("Engine", "Clouds of Orange Juice");
                    break;
                default:
                    break;
            }

            this.show();
        }

        public override void OnUnload()
        {
            
        }

        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            Background.Update(mouse, keyBoard, gameTime);
        }
    }
}
