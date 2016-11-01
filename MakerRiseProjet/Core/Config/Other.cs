using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.Config
{
    class Other
    {

        public static Storage.DataSheet DS = new Storage.DataSheet("Game\\Config\\Input.rise");
        public static void Load()
        {
            Core.Debug.DebugLogs.WriteInLogs("[Config.Controls] Load config...", Core.Debug.LogType.Info);
            DS.Load();


        }
    }
}
