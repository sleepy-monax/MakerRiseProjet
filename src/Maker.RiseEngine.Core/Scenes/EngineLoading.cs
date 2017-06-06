using Maker.RiseEngine.Config;

using Maker.RiseEngine.Input;
using Maker.RiseEngine.Plugin;
using Maker.RiseEngine.Rendering;
using Maker.RiseEngine.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Windows.Forms;
using static Maker.RiseEngine.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Scenes.Scenes
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
                Debug.WriteLog("Initializing...", LogType.Info, "Engine");
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
                PluginLoader pluginLoader = new PluginLoader(Engine, Engine.userConfig.EngineSelectedProfil);
                pluginLoader.InitializePlugin();
                Rise.Engine.loadedPlugins = pluginLoader.Plugins;

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
