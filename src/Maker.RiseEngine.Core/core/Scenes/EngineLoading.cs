using Maker.RiseEngine.Core.Config;

using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Windows.Forms;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.Scenes.Scenes
{
    public class EngineLoading : Scene
    {

        public string Message = "Loading...";

        public override void OnLoad()
        {
            Show();

            Engine.songManager.SwitchSong("Engine", "rise");

            ThreadStart GenHandle = new ThreadStart(delegate
            {
                EngineDebug.DebugLogs.WriteLog("Initializing...", EngineDebug.LogType.Info, "Engine");
                Thread.Sleep(Engine.userConfig.EngineSplashScreenTime);

                Message = "Loading config...";

                // load binary config file.
                if (System.IO.File.Exists("config.bin"))
                    Engine.userConfig = SerializationHelper.LoadFromBin<EngineUserConfig>("config.bin");
                else
                    SerializationHelper.SaveToBin(Engine.userConfig, "config.bin");

                //setting up screen
                if (Engine.userConfig.GraphicsEnableFullscreen == true)
                {
                    // Set full screen.
                    Engine.graphicsDeviceManager.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
                    Engine.graphicsDeviceManager.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
                    Engine.graphicsDeviceManager.ApplyChanges();
                    Engine.graphicsDeviceManager.ToggleFullScreen();
                }

                this.Message = "Loading Plugins...";
                PluginLoader p = new PluginLoader(Engine.userConfig.EngineSelectedProfil);
                p.InitializePlugin();
                Rise.Plugins = p.Plugins;
  

                Engine.sceneManager.RemoveScene(this);
            });
            Thread t = new Thread(GenHandle);
            t.Start();
        }

        public override void OnUnload()
        {

        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Engine.ressourceManager.GetTexture2D("Engine", "MakerLogo"), new Rectangle(0,0, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), Color.White);
            spriteBatch.DrawString(Engine.ressourceManager.GetSpriteFont("Engine", "Consolas_16pt"), Message, new Rectangle(0, 16, Engine.graphicsDeviceManager.PreferredBackBufferWidth, Engine.graphicsDeviceManager.PreferredBackBufferHeight), Alignment.Top, Style.DropShadow, Color.White);
        }
    }
}
