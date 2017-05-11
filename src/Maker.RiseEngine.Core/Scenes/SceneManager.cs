using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Maker.RiseEngine.Core.Rendering;


namespace Maker.RiseEngine.Core.Scenes
{
    public class SceneManager : IDrawable
    {

        engine ENGINE;
        List<Scene> Scenes;

        public SceneManager(engine engine)
        {

            ENGINE = engine;
            Scenes = new List<Scene>();

        }

        public void AddScene(Scene scene)
        {
            EngineDebug.DebugLogs.WriteLog($"Switching to {scene.GetType().Name}", EngineDebug.LogType.Info, nameof(SceneManager));
            try
            {
                scene.RiseEngine = ENGINE;
                rise.GameForm.Invoke(new MethodInvoker(() => scene.OnLoad()));
            }
            catch (Exception ex)
            {
                EngineDebug.DebugLogs.WriteLog($"Error append during scene loading : \n{ex.ToString()}", EngineDebug.LogType.Error, "SceneManager");
            }

            Scenes.Add(scene);
        }

        public void RemoveScene(Scene scene)
        {

            try
            {
                rise.GameForm.Invoke(new MethodInvoker(() => scene.OnUnload()));
            }
            catch (Exception ex)
            {
                EngineDebug.DebugLogs.WriteLog($"Error append during scene unloading : \n{ex.ToString()}", EngineDebug.LogType.Error, "SceneManager");

                throw;
            }
            Scenes.Remove(scene);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            try
            {
                foreach (Scene s in Scenes)
                {
                    spriteBatch.Begin();
                    s.sceneDraw(spriteBatch, gameTime);


                    spriteBatch.End();
                }
                if (rise.engineConfig.Debug_SceneManager)
                {
                    spriteBatch.Begin();

                    spriteBatch.FillRectangle(new Rectangle(16, 48, 256, 16 + 32 * (Scenes.Count + 1)), new Color(Color.Black, 0.4f));
                    spriteBatch.DrawString(ENGINE.RESSOUCES.GetSpriteFont("Engine", "segoeUI_16pt"), "Loaded scenes :", new Rectangle(24, 48, 256, 32), Alignment.Left, Style.DropShadow, Color.White);

                    int i = 1;
                    foreach (Scene s in Scenes)
                    {

                        spriteBatch.DrawString(ENGINE.RESSOUCES.GetSpriteFont("Engine", "segoeUI_16pt"), s.GetType().Name, new Rectangle(24, (32 * i) + 48, 256, 32), Alignment.Left, Style.DropShadow, Color.White);

                        i++;
                    }

                    spriteBatch.End();
                }
            }
            catch (Exception ex)
            {
                if (rise.engineConfig.Debug_ShowErrorMessages)
                    MessageBox.Show(ex.ToString());
            }


        }

        public void Update(GameInput playerInput, GameTime gameTime)
        {

            try
            {
                foreach (Scene s in Scenes)
                {
                    s.sceneUpdate(playerInput, gameTime);
                }
            }
            catch (Exception ex)
            {
                if (rise.engineConfig.Debug_ShowErrorMessages)
                    MessageBox.Show(ex.ToString());
            }
        }
    }
}
