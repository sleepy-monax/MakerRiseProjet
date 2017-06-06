using Maker.RiseEngine;
using Maker.RiseEngine.Input;
using Maker.RiseEngine.MathExt;
using Maker.RiseEngine.Rendering;
using Maker.RiseEngine.Scenes;
using Maker.RiseEngine.Storage;

using Maker.Twiyol.Game.GameUtils;
using Maker.Twiyol.Game.WorldDataStruct;
using Maker.Twiyol.Generator;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.IO;

namespace Maker.Twiyol.Game
{
    public class GameScene : Scene
    {
        public DataWorld World;
        public ChunkDecorator chunkDecorator;

        public Random Rnd;

        public GameCamera Camera;

        public WorldRender worldRender;

        public WorldUpdater worldUpdater;
        public EventsManager eventsManager;
        public MiniMap miniMap;

        public GameUIScene GameUIScene;

        public bool PauseSimulation = false;

        RenderTarget2D WorldRenderTarget;

        public GameScene(DataWorld world)
        {
            World = world;
            Rnd = new Random(World.Seed);
            chunkDecorator = new Generator.ChunkDecorator(this, Rnd);
            worldUpdater = new WorldUpdater(this);
            eventsManager = new EventsManager(this);
            miniMap = new MiniMap(this);
            Camera = new GameCamera(this);
            GameUIScene = new GameUIScene(this);
        }

        // Implement interface.
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            worldRender.DrawWorld(WorldRenderTarget, gameTime);
            
            if (PauseSimulation)
            {
                spriteBatch.Draw(WorldRenderTarget, new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), Color.White);
                spriteBatch.Draw(WorldRenderTarget, new Rectangle(1, 1, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), new Color(255, 255, 255, 150));
                spriteBatch.FillRectangle(new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), new Color(0, 0, 0, 150));
            }
            else
            {
                spriteBatch.Draw(WorldRenderTarget, new Rectangle(0, 0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), Color.White);
            }
        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            // Update game.
            if (!PauseSimulation)
            {
                worldUpdater.Update(playerInput, gameTime);
            }

            // Take screenshots.
            if (playerInput.IsKeyBoardKeyPress(Engine.userConfig.InputScreenshot)) {
                
                string path = $"Screenshots\\{RandomHelper.RandomString(16).ToLower()}.png";
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                WorldRenderTarget.SaveAsPng(fs, WorldRenderTarget.Width, WorldRenderTarget.Height);
                fs.Close();
            }

            Camera.Update();
        }

        public override void OnLoad()
        {
            worldRender = new WorldRender(Engine, this);
            WorldRenderTarget = new RenderTarget2D(
                Engine.GraphicsDevice,
                Engine.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Engine.GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                Engine.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            Engine.songManager.SwitchSong("Engine", "A Title");
            Engine.sceneManager.AddScene(GameUIScene);
            GameUIScene.Show();
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
