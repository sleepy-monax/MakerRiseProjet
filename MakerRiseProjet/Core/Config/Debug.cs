using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.Config
{
    public static class Debug
    {

        public static bool FrameCounter = false;
        public static bool GuiFrame =   false;
        public static bool WorldOverDraw = false;
        public static bool WorldFocusLocation = false;
        public static bool DebugWaterMark = false;

        public static bool EnableDebugLogs = false;

        public static Storage.DataSheet DS = new Storage.DataSheet("Data\\Engine\\Config\\Debug.rise");

        public static void Load()
        {
            Core.Debug.Logs.Write("[Config.Debugs] Load config...", Core.Debug.LogType.Info);
            DS.Load();

            FrameCounter = Convert.ToBoolean(int.Parse(DS.GetData("FrameCounter", "0")));
            GuiFrame = Convert.ToBoolean(int.Parse(DS.GetData("GuiFrame", "0")));
            WorldOverDraw = Convert.ToBoolean(int.Parse(DS.GetData("WorldOverDraw", "0")));
            WorldFocusLocation = Convert.ToBoolean(int.Parse(DS.GetData("WorldFocusLocation", "0")));
            DebugWaterMark = Convert.ToBoolean(int.Parse(DS.GetData("DebugWaterMark", "0")));

            DS.Save();



        }
         
    }
}
