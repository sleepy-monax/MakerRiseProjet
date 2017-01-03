using Maker.RiseEngine.Core.Scenes.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Maker.RiseEngine.Core
{
    public static class Engine
    {

        

        public static GraphicsDeviceManager graphics;
        public static Game MainGame;
        public static GraphicsDevice GraphicsDevice;
        public static string SaveLocation = "Save";
        public static GameWindow Window;
        public static Form GameForm;

        public static Version Version = Assembly.GetExecutingAssembly().GetName().Version;
        public static bool AsErrore = false;
        public static bool IsLoaded = false;
        public static int CurrentFrame = 0;
        public static UI.Cursor MouseCursor;

        public static Config.EngineConfig engineConfig = new Config.EngineConfig();

        public static void Initialize(EngineLoading LoadingScene)
        {

            EngineDebug.DebugLogs.WriteInLogs("Initializing...", EngineDebug.LogType.Info, "Engine");

            LoadingScene.Message = "Loading config...";

            // load binary config file.
            if (System.IO.File.Exists("Data\\config.bin"))
                engineConfig = Storage.SerializationHelper.LoadFromBin<Config.EngineConfig>("Data\\config.bin");
            else
                Storage.SerializationHelper.SaveToBin(engineConfig, "Data\\config.bin");

            // Apply config



            Thread.Sleep(200);

            MouseCursor = new UI.Cursor();

            LoadingScene.Message = "Loading Plugins...";
            GameObjectsManager.LoadPlugins();
            Thread.Sleep(200);

            LoadingScene.Message = "Looking for initialization error...";
            if (GameObjectsManager.IsFullLoaded())
            {

                EngineDebug.DebugLogs.WriteInLogs("Initializing Done !", EngineDebug.LogType.Info, "Engine");
            }
            else
            {
                EngineDebug.DebugLogs.WriteInLogs("Initializing Failed !", EngineDebug.LogType.Info, "Engine");

                // Get user input after fatal error :D.
                switch (System.Windows.Forms.MessageBox.Show("Gosh !" + Environment.NewLine + "An error that occurred during initialization of the engine.", "MakerRiseEngine " + Version, System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore, System.Windows.Forms.MessageBoxIcon.Error))
                {
                    // The user went to extit the game.
                    case System.Windows.Forms.DialogResult.Abort:
                        System.Windows.Forms.Application.Exit();
                        break;

                    // The user went to restart the  game engine
                    case System.Windows.Forms.DialogResult.Retry:
                        System.Windows.Forms.Application.Restart();
                        break;
                    
                    // The user ignore the fatal error 😏.
                    case System.Windows.Forms.DialogResult.Ignore:
                        AsErrore = true;
                        break;
                }


            }
            Thread.Sleep(200);
            LoadingScene.Message = "Starting game...";
            IsLoaded = true;

        }

    }
}
