using Maker.RiseEngine.Core.Game.World;
using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Maker.RiseEngine.Core.Game
{
    public class GameScene : SceneManager.Scene
    {
        public ObjWorld world;
        public Generator.ChunkDecorator chunkDecorator;

        public Random Rnd;
        public MathExt.Noise.PerlinNoise Noise;

        public GameUtils.GameCamera Camera;
        public Rectangle SelectionRect;

        public GameUtils.WorldRender worldRender;

        public GameUtils.WorldUpdater worldUpdater;
        public GameUtils.ChunkManager chunkManager;
        public GameUtils.WorldProperty worldProperty;
        public GameUtils.EventsManager eventsManager;
        public GameUtils.EntityManager entityManager;
        public GameUtils.MiniMap miniMap;
        public GameUtils.GameUI gameUI;
        public GameUtils.SaveFile saveFile;

        SpriteBatch BackgroundSB;
        Parallax Background;

        public bool Pause = false;

        public GameScene(GameUtils.WorldProperty _worldProperty, Random _Rnd)
        {
            world = new ObjWorld();

            saveFile = new GameUtils.SaveFile(this);
            worldProperty = _worldProperty;
            Rnd = _Rnd;
            chunkDecorator = new Generator.ChunkDecorator(this, Rnd);
            Noise = new MathExt.Noise.PerlinNoise(worldProperty.Seed);

            worldUpdater = new GameUtils.WorldUpdater(this);
            chunkManager = new GameUtils.ChunkManager(this);
            eventsManager = new GameUtils.EventsManager(this);
            entityManager = new GameUtils.EntityManager(this);
            miniMap = new GameUtils.MiniMap(this);
            gameUI = new GameUtils.GameUI(this);

            Camera = new GameUtils.GameCamera(this);

            worldRender = new GameUtils.WorldRender(this);

            BackgroundSB = new SpriteBatch(Engine.GraphicsDevice);
            Background = Rendering.ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));

        }



        public void TogglePauseGame()
        {

            Pause = !Pause;

            if (Pause)
            {
                gameUI.cManager.SwitchContainer("PauseMenu");
            }
            else
            {
                gameUI.cManager.SwitchContainer("GameUI");
            }

        }

        // Implement interface.
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, gameTime);
            BackgroundSB.End();

            worldRender.Draw(gameTime, Pause);


            if (Pause)
            {
                spriteBatch.FillRectangle(new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 150));

            }

            gameUI.Draw(spriteBatch, gameTime);

        }

        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            if (!Pause)
            {
                Background.Update(mouse, keyBoard, gameTime);
                worldUpdater.Update(mouse, keyBoard, gameTime);
            }
            Camera.Update();
            gameUI.Update(mouse, keyBoard, gameTime);
        }

        public override void OnLoad()
        {
            Audio.SongEngine.SwitchSong("Engine", "A Title");
        }

        public override void OnUnload()
        {
            throw new NotImplementedException();
        }
    }
}
