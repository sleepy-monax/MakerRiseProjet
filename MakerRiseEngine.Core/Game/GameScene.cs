using Maker.RiseEngine.Core.Game.World;
using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Maker.RiseEngine.Core.Game
{
    public class GameScene : Idrawable
    {
        public ObjWorld world;
        public Generator.ChunkDecorator chunkDecorator;

        public Random Rnd;
        public GameMath.Noise.PerlinNoise Noise;

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
            Noise = new GameMath.Noise.PerlinNoise(worldProperty.Seed);

            worldUpdater = new GameUtils.WorldUpdater(this);
            chunkManager = new GameUtils.ChunkManager(this);
            eventsManager = new GameUtils.EventsManager(this);
            entityManager = new GameUtils.EntityManager(this);
            miniMap = new GameUtils.MiniMap(this);
            gameUI = new GameUtils.GameUI(this);

            Camera = new GameUtils.GameCamera(this);

            worldRender = new GameUtils.WorldRender(this);

            BackgroundSB = new SpriteBatch(Common.GraphicsDevice);
            Background = Rendering.ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight));

        }

        public void Update(MouseState _Mouse, KeyboardState _KeyBoard, GameTime _GameTime)
        {


            if (!Pause)
            {
                Background.Update(_Mouse, _KeyBoard, _GameTime);
                worldUpdater.Update(_Mouse, _KeyBoard, _GameTime);
            }
            Camera.Update();
            gameUI.Update(_Mouse, _KeyBoard, _GameTime);



        }



        public void Draw(SpriteBatch _SpriteBatch, GameTime _GameTime)
        {

            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, _GameTime);
            BackgroundSB.End();

            worldRender.Draw(_GameTime, Pause);


            if (Pause)
            {
                _SpriteBatch.FillRectangle(new Rectangle(0, 0, Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 150));

            }

            gameUI.Draw(_SpriteBatch, _GameTime);
        }

        public void startGame()
        {



        }

        public void StopGame()
        {

            Scene.SceneManager.CurrentScene = 0;
            Scene.SceneManager.Gm = null;
            GC.Collect();

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
    }
}
