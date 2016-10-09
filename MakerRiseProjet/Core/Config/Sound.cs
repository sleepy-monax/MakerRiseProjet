using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.Config
{
    class Sound
    {

        public static float Master = 1f;
        public static float Songs = 1f;
        public static float Effects = 0.5f;
        

        public static Storage.DataSheet DS = new Storage.DataSheet("Game\\Config\\Input.rise");
        public static void Load()
        {
            Core.Debug.Logs.Write("[Config.Controls] Load config...", Core.Debug.LogType.Info);
            DS.Load();
            

        }
    }
}
