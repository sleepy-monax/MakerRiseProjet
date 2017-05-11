using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.MathExt;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.Storage;

using Maker.Twiyol.Game.WorldDataStruct;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.IO;

namespace Maker.Twiyol.Game
{
    public class GameScene : Scene
    {
        public DataWorld World;
        public Generator.ChunkDecorator chunkDecorator;

        public Random Rnd;

        public GameUtils.GameCamera Camera;
        public Rectangle SelectionRect;

        public GameUtils.WorldRender worldRender;

        public GameUtils.WorldUpdater worldUpdater;
        public GameUtils.EventsManager eventsManager;
        public GameUtils.MiniMap miniMap;

        public GameUIScene GameUIScene;
        SpriteBatch BackgroundSB;
        Parallax Background;

        public bool PauseSimulation = false;

        RenderTarget2D WorldRenderTarget;

        public GameScene(DataWorld world)
        {
            World = world;
            Rnd = new Random(World.Seed);
            chunkDecorator = new Generator.ChunkDecorator(this, Rnd);

            worldUpdater = new GameUtils.WorldUpdater(this);
            eventsManager = new GameUtils.EventsManager(this);
            miniMap = new GameUtils.MiniMap(this);

            Camera = new GameUtils.GameCamera(this);

            worldRender = new GameUtils.WorldRender(this);

            BackgroundSB = new SpriteBatch(rise.GraphicsDevice);
            Background = ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, rise.graphics.PreferredBackBufferWidth, rise.graphics.PreferredBackBufferHeight));

            GameUIScene = new GameUIScene(this);

            WorldRenderTarget = new RenderTarget2D(
                rise.GraphicsDevice,
                rise.GraphicsDevice.PresentationParameters.BackBufferWidth,
                rise.GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                rise.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
        }

        // Implement interface.
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, gameTime);
            BackgroundSB.End();

            worldRender.Draw(WorldRenderTarget,gameTime);
            spriteBatch.Draw(WorldRenderTarget, new Rectangle(0, 0, rise.graphics.PreferredBackBufferWidth, rise.graphics.PreferredBackBufferHeight), Color.White);


            if (PauseSimulation)
            {
                spriteBatch.FillRectangle(new Rectangle(0, 0, rise.graphics.PreferredBackBufferWidth, rise.graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 150));

            }
        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {

            // Update game.
            if (!PauseSimulation)
            {
                Background.Update(playerInput, gameTime);
                worldUpdater.Update(playerInput, gameTime);

            }

            // Take screenshots.
            if (playerInput.IsKeyBoardKeyPress(rise.engineConfig.Input_Take_Screenshot)) {
                
                string path = $"Screenshots\\{RandomHelper.RandomString(16).ToLower()}.png";
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                WorldRenderTarget.SaveAsPng(fs, WorldRenderTarget.Width, WorldRenderTarget.Height);
                fs.Close();
            }


            Camera.Update();
        }

        public override void OnLoad()
        {
            RiseEngine.SONGS.SwitchSong("Engine", "A Title");
            RiseEngine.ScenesManager.AddScene(GameUIScene);
            GameUIScene.show();
        }

        public override void OnUnload()
        {

        }

        public void SaveWorld()
        {
            SerializationHelper.SaveToBin(World, $"Saves/{World.Name}.bin");
        }
    }
}
