using Maker.RiseEngine.Core.Config;
using Maker.RiseEngine.Core.Plugin;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Maker.RiseEngine.Core
{
    public static class Rise
    {

        public static Dictionary<string, IPlugin> Plugins;
        public static GameEngine Engine;
        public static GameWindow Window;
        public static Form GameForm;

        public static Version Version = new Version(1,0,0);
        public static int CurrentFrame = 0;

        

        public static void STOP()
        {
            Environment.Exit(0);
        }
    }
}
