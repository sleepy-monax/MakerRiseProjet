﻿using Maker.twiyol.Game.WorldDataStruct;
using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Audio;
using Maker.RiseEngine.Core.Storage;

namespace Maker.twiyol.Game
{
    public class GameScene : Scene
    {
        public DataWorld world;
        public Generator.ChunkDecorator chunkDecorator;

        public Random Rnd;

        public GameUtils.GameCamera Camera;
        public Rectangle SelectionRect;

        public GameUtils.WorldRender worldRender;

        public GameUtils.WorldUpdater worldUpdater;
        public GameUtils.ChunkManager chunkManager;
        public GameUtils.WorldProperty worldProperty;
        public GameUtils.EventsManager eventsManager;
        public GameUtils.EntityDataManager EntityDataManager;
        public GameUtils.MiniMap miniMap;
        public GameUtils.SaveFile saveFile;

        public GameUIScene GameUIScene;
        SpriteBatch BackgroundSB;
        Parallax Background;

        public bool PauseSimulation = false;

        public GameScene(GameUtils.WorldProperty _worldProperty, Random _Rnd)
        {
            world = new DataWorld();

            saveFile = new GameUtils.SaveFile(this);
            worldProperty = _worldProperty;
            Rnd = _Rnd;
            chunkDecorator = new Generator.ChunkDecorator(this, Rnd);

            worldUpdater = new GameUtils.WorldUpdater(this);
            chunkManager = new GameUtils.ChunkManager(this);
            eventsManager = new GameUtils.EventsManager(this);
            EntityDataManager = new GameUtils.EntityDataManager(this);
            miniMap = new GameUtils.MiniMap(this);

            Camera = new GameUtils.GameCamera(this);

            worldRender = new GameUtils.WorldRender(this);

            BackgroundSB = new SpriteBatch(Engine.GraphicsDevice);
            Background = ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));

            GameUIScene = new GameUIScene(this);
        }

        // Implement interface.
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, gameTime);
            BackgroundSB.End();

            worldRender.Draw(gameTime);


            if (PauseSimulation)
            {
                spriteBatch.FillRectangle(new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 150));

            }


        }

        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            if (!PauseSimulation)
            {
                Background.Update(mouse, keyBoard, gameTime);
                worldUpdater.Update(mouse, keyBoard, gameTime);
            }
            Camera.Update();
        }

        public override void OnLoad()
        {
            SongEngine.SwitchSong("Engine", "A Title");
            Game.sceneManager.AddScene(GameUIScene);
            GameUIScene.show();
        }

        public override void OnUnload()
        {

        }

        public void SaveWorld()
        {
            SerializationHelper.SaveToBin(world, "world.bin");
        }
    }
}