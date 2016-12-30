using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.SceneManager
{
    public class SceneManager : Idrawable
    {

        RiseGame Game;
        List<Scene> Scenes;
        List<Scene> ScenesToRemove;
        List<Scene> ScenesToAdd;

        public SceneManager(RiseGame game)
        {

            Game = game;
            Scenes = new List<Scene>();
            ScenesToRemove = new List<Scene>();
            ScenesToAdd = new List<Scene>();

        }

        public void AddScene(Scene scene)
        {
            EngineDebug.DebugLogs.WriteInLogs($"Switching to {scene.GetType().Name}", EngineDebug.LogType.Info, nameof(SceneManager));
            try
            {
                scene.Game = Game;
                scene.OnLoad();
            }
            catch (Exception ex)
            {
                EngineDebug.DebugLogs.WriteInLogs($"Error append during scene loading : \n{ex.ToString()}", EngineDebug.LogType.Error, "SceneManager");
            }

            ScenesToAdd.Add(scene);
        }

        public void RemoveScene(Scene scene)
        {

            try
            {
                scene.OnUnload();
            }
            catch (Exception ex)
            {
                EngineDebug.DebugLogs.WriteInLogs($"Error append during scene unloading : \n{ex.ToString()}", EngineDebug.LogType.Error, "SceneManager");

                throw;
            }
            ScenesToRemove.Add(scene);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Scene s in Scenes)
            {
                s.sceneDraw(spriteBatch, gameTime);
            }
        }

        public void Update(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            foreach (Scene s in ScenesToAdd) {
                Scenes.Add(s);
                
            }

            ScenesToAdd.Clear();

            foreach (Scene s in Scenes)
            {
                s.sceneUpdate(mouse, keyBoard, gameTime);
            }

            foreach (Scene s in ScenesToRemove) {
                Scenes.Remove(s);
            }

            ScenesToRemove.Clear();

        }
    }
}
