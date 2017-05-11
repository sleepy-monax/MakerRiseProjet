using Maker.RiseEngine.Core.Config;
using Maker.RiseEngine.Core.Ressources;
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


            show();

            rise.ENGINE.SONGS.SwitchSong("Engine", "rise");

            ThreadStart GenHandle = new ThreadStart(delegate
            {
                Thread.Sleep(3000);
                EngineDebug.DebugLogs.WriteLog("Initializing...", EngineDebug.LogType.Info, "Engine");
                Thread.Sleep(rise.engineConfig.Engine_SplashTime);

                Message = "Loading config...";

                // load binary config file.
                if (System.IO.File.Exists("config.bin"))
                    rise.engineConfig = SerializationHelper.LoadFromBin<EngineConfig>("config.bin");
                else
                    SerializationHelper.SaveToBin(rise.engineConfig, "config.bin");
                this.Message = "Loading Plugins...";
                PluginLoader<IPlugin> p = new PluginLoader<IPlugin>("Plugins");
                p.initializePlugin();
                rise.Plugins = p.Plugins;
  

                rise.IsLoaded = true;

                ENGINE.SCENES.RemoveScene(this);
            });
            Thread t = new Thread(GenHandle);
            t.Start();
        }

        public override void OnUnload()
        {
            //setting up screen
            if (rise.engineConfig.GFX_FullScreen == true)
            {
                // Set full screen.
                rise.graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
                rise.graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
                rise.graphics.ToggleFullScreen();
                rise.graphics.ApplyChanges();
            }
            else
            {
                // Set window setting.
                rise.graphics.PreferredBackBufferWidth = 1366;
                rise.graphics.PreferredBackBufferHeight = 768;
                rise.graphics.ApplyChanges();
            }
        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(ENGINE.RESSOUCES.GetTexture2D("Engine", "MakerLogo"), new Rectangle(0,0, rise.graphics.PreferredBackBufferWidth, rise.graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.DrawString(ENGINE.RESSOUCES.SpriteFont("Engine", "Consolas_16pt"), Message, new Rectangle(0, 16, rise.graphics.PreferredBackBufferWidth, rise.graphics.PreferredBackBufferHeight), Alignment.Top, Style.DropShadow, Color.White);
        }
    }
}
