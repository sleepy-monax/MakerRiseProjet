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
        public static GameWindow Window;
        public static Form GameForm;

        public static Version Version = Assembly.GetExecutingAssembly().GetName().Version;
        public static bool AsErrore = false;
        public static bool IsLoaded = false;
        public static int CurrentFrame = 0;


        public static Config.EngineConfig engineConfig = new Config.EngineConfig();
    }
}
