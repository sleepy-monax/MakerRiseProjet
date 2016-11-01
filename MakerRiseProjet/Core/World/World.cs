using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RiseEngine.Core.World.Obj;
using RiseEngine.Core.Rendering;

namespace RiseEngine.Core.World
{
    public class WorldScene : Idrawable
    {

        public ObjChunk[,] Chunks;
        public Dictionary<int, ObjRegion> Region;
        public Generator.ChunkDecorator chunkDecorator;

        public Random Rnd;
        public GameMath.Noise.PerlinNoise Noise;

        public Utils.GameCamera Camera;
        public Rectangle SelectionRect;

        public Utils.WorldRender worldRender;

        public Utils.WorldUpdater worldUpdater;
        public Utils.ChunkManager chunkManager;
        public Utils.WorldProperty worldProperty;
        public Utils.EventsManager eventsManager;
        public Utils.EntityManager entityManager;
        public Utils.MiniMap miniMap;
        public Utils.GameUI gameUI;
        public Utils.SaveFile saveFile;

        SpriteBatch BackgroundSB;
        Parallax Background;

        public bool Pause = false;

        public WorldScene(Utils.WorldProperty _worldProperty, Random _Rnd)
        {
            saveFile = new Utils.SaveFile(this);
            worldProperty = _worldProperty;
            Rnd = _Rnd;
            Region = new Dictionary<int, ObjRegion>();
            chunkDecorator = new Generator.ChunkDecorator(this, Rnd);
            Noise = new GameMath.Noise.PerlinNoise(worldProperty.Seed);



            worldUpdater = new Utils.WorldUpdater(this);
            chunkManager = new Utils.ChunkManager(this);
            eventsManager = new Utils.EventsManager(this);
            entityManager = new Utils.EntityManager(this);
            miniMap = new Utils.MiniMap(this);
            gameUI = new Utils.GameUI(this);

            Camera = new Utils.GameCamera(this);

            worldRender = new Utils.WorldRender(this);

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
