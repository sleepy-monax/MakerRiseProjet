using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Maker.RiseEngine.Rendering;
using Maker.RiseEngine.Scenes;
using Maker.RiseEngine;
using Maker.RiseEngine.Input;

namespace Maker.Twiyol.Scenes.Menu
{
    public class MainMenuBackground : Scene
    {

        // BackGround.
        SpriteBatch BackgroundSB;
        Parallax Background;

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, gameTime);
            BackgroundSB.End();
            spriteBatch.FillRectangle(new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), new Color(0, 0, 0, 128));
        }

        public override void OnLoad()
        {

            //Back Ground.
            BackgroundSB = new SpriteBatch(Engine.GraphicsDevice);
            switch (new Random().Next(3))
            {
                case 0:
                    Background = ParallaxParse.Parse("Engine", "Dusk Mountain", new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight));
                    Engine.songManager.SwitchSong("Engine", "A Title");
                    break;
                case 1:
                    Background = ParallaxParse.Parse("Engine", "Forest", new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight));
                    Engine.songManager.SwitchSong("Engine", "Look Up");
                    break;
                case 2:
                    Background = ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight));
                    Engine.songManager.SwitchSong("Engine", "Clouds of Orange Juice");
                    break;
                default:
                    break;
            }

            this.Show();
        }

        public override void OnUnload()
        {

        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            Background.DestinationRectangle = new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight);
            Background.Update(playerInput, gameTime);
        }
    }
}
