using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Config
{
    class Gfx
    {

        public static int ViewDistance = 32;
        public static bool FullScreen = false;

        public static Storage.DataSheet DS = new Storage.DataSheet("Game\\Config\\Input.rise");
        public static void Load()
        {
            Core.EngineDebug.DebugLogs.WriteInLogs("Load config...", Core.EngineDebug.LogType.Info, "Config.Controls");
            DS.Load();
        }
    }
}
