using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.SceneManager
{
    public class SceneManager : Idrawable
    {

        RiseGame Game;
        List<Scene> Scenes;

        public SceneManager(RiseGame game)
        {

            Game = game;
            Scenes = new List<Scene>();

        }

        public void AddScene(Scene scene)
        {

            try
            {
                scene.Game = Game;
                scene.OnLoad();
            }
            catch (Exception ex)
            {
                EngineDebug.DebugLogs.WriteInLogs($"Error append during scene loading : \n{ex.ToString()}", EngineDebug.LogType.Error, "SceneManager");
            }

            Scenes.Add(scene);
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

            Scenes.Remove(scene);
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
            foreach (Scene s in Scenes)
            {
                s.sceneUpdate(mouse, keyBoard, gameTime);
            }
        }
    }
}
