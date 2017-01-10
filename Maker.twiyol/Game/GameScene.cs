using Maker.twiyol.Game.WorldDataStruct;
using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Audio;
using Maker.RiseEngine.Core.Storage;
using Maker.RiseEngine.Core.Input;
using System.IO;
using Maker.RiseEngine.Core.MathExt;

namespace Maker.twiyol.Game
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
        public GameUtils.SaveFile saveFile;

        public GameUIScene GameUIScene;
        SpriteBatch BackgroundSB;
        Parallax Background;

        public bool PauseSimulation = false;

        RenderTarget2D WorldRenderTarget;

        public GameScene(DataWorld world)
        {
            World = world;
            saveFile = new GameUtils.SaveFile(this);
            Rnd = new Random(World.Seed);
            chunkDecorator = new Generator.ChunkDecorator(this, Rnd);

            worldUpdater = new GameUtils.WorldUpdater(this);
            eventsManager = new GameUtils.EventsManager(this);
            miniMap = new GameUtils.MiniMap(this);

            Camera = new GameUtils.GameCamera(this);

            worldRender = new GameUtils.WorldRender(this);

            BackgroundSB = new SpriteBatch(Engine.GraphicsDevice);
            Background = ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));

            GameUIScene = new GameUIScene(this);

            WorldRenderTarget = new RenderTarget2D(
                Engine.GraphicsDevice,
                Engine.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Engine.GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                Engine.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);
        }

        // Implement interface.
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, gameTime);
            BackgroundSB.End();

            worldRender.Draw(WorldRenderTarget,gameTime);
            spriteBatch.Draw(WorldRenderTarget, new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Color.White);


            if (PauseSimulation)
            {
                spriteBatch.FillRectangle(new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 150));

            }
        }

        public override void OnUpdate(PlayerInput playerInput, GameTime gameTime)
        {
            // Update game.
            if (!PauseSimulation)
            {
                Background.Update(playerInput, gameTime);
                worldUpdater.Update(playerInput, gameTime);

            }

            // Take screenshots.
            if (playerInput.IsKeyBoardKeyReleased(Engine.engineConfig.Input_Take_Screenshot)) {
                
                string path = $"Screenshots\\{RandomHelper.RandomString(16).ToLower()}.png";
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                WorldRenderTarget.SaveAsPng(fs, WorldRenderTarget.Width, WorldRenderTarget.Height);
                fs.Close();
            }


            Camera.Update();
        }

        public override void OnLoad()
        {
            SongEngine.SwitchSong("Engine", "A Title");
            RiseEngine.sceneManager.AddScene(GameUIScene);
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
