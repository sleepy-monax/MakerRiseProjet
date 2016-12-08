namespace Maker.RiseEngine.Core.Config
{
    class Other
    {

        public static Storage.DataSheet DS = new Storage.DataSheet("Game\\Config\\Input.rise");
        public static void Load()
        {
            Core.EngineDebug.DebugLogs.WriteInLogs("Load config...", Core.EngineDebug.LogType.Info, "Config.Controls");
            DS.Load();
        }
    }
}
