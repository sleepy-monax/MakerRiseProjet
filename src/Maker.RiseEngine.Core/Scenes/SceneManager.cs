﻿using Maker.RiseEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Maker.RiseEngine.Rendering;


namespace Maker.RiseEngine.Scenes
{
    public class SceneManager
    {

        GameEngine Engine;
        List<Scene> Scenes;

        public SceneManager(GameEngine engine)
        {

            Engine = engine;
            Scenes = new List<Scene>();

        }

        public void AddScene(Scene scene)
        {
            Debug.WriteLog($"Switching to {scene.GetType().Name}", LogType.Info, nameof(SceneManager));
            try
            {
                scene.Engine = Engine;
                Rise.Engine.gameForm.Invoke(new MethodInvoker(() => scene.OnLoad()));
            }
            catch (Exception ex)
            {
                Debug.WriteLog($"Error append during scene loading : \n{ex.ToString()}", LogType.Error, "SceneManager");
            }

            Scenes.Add(scene);
        }

        public void RemoveScene(Scene scene)
        {

            try
            {
                Rise.Engine.gameForm.Invoke(new MethodInvoker(() => scene.OnUnload()));
            }
            catch (Exception ex)
            {
                Debug.WriteLog($"Error append during scene unloading : \n{ex.ToString()}", LogType.Error, "SceneManager");

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
                    s.Draw(spriteBatch, gameTime);


                    spriteBatch.End();
                }
                if (Engine.userConfig.DebugShowLoadedSceneList)
                {
                    spriteBatch.Begin();

                    spriteBatch.FillRectangle(new Rectangle(16, 48, 256, 16 + 32 * (Scenes.Count + 1)), new Color(Color.Black, 0.4f));
                    spriteBatch.DrawString(Engine.ressourceManager.GetSpriteFont("Engine", "segoeUI_16pt"), "Loaded scenes :", new Rectangle(24, 48, 256, 32), Alignment.Left, Style.DropShadow, Color.White);

                    int i = 1;
                    foreach (Scene s in Scenes)
                    {

                        spriteBatch.DrawString(Engine.ressourceManager.GetSpriteFont("Engine", "segoeUI_16pt"), s.GetType().Name, new Rectangle(24, (32 * i) + 48, 256, 32), Alignment.Left, Style.DropShadow, Color.White);

                        i++;
                    }

                    spriteBatch.End();
                }
            }
            catch (Exception ex)
            {
                if (Engine.userConfig.DebugShowErrorMessages)
                    MessageBox.Show(ex.ToString());
            }


        }

        public void Update(GameInput playerInput, GameTime gameTime)
        {

            try
            {
                foreach (Scene s in Scenes)
                {
                    s.Update(playerInput, gameTime);
                }
            }
            catch (Exception ex)
            {
                if (Engine.userConfig.DebugShowErrorMessages)
                    MessageBox.Show(ex.ToString());
            }
        }
    }
}
