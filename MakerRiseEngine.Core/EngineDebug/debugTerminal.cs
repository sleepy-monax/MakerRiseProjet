using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.EngineDebug
{
    public class debugTerminal
    {
        Thread t;

        public debugTerminal()
        {

            ThreadStart GenHandle = new ThreadStart(delegate
            {

                DebugLogs.WriteLog("Debug thread stated !", LogType.Info, "DEBUG");

                do
                {

                    var text = Console.ReadLine();

                    if (text == null) {
                        break;
                    }

                    switch (text.ToLower())
                    {
                        case "stop":
                            // This function stop the game engine.
                            DebugLogs.WriteLog("Stop game engine !", LogType.Info, "$");
                            Engine.STOP();
                            break;

                        case "plug":
                            DebugLogs.WriteLog("Usage : -list, -info", LogType.Info, "$");
                            break;

                        case "plug -list":
                            // This function show a list of all loaded plugin.
                            DebugLogs.WriteLog("This is the list of loaded plugins :", LogType.Info, "$");
                            DebugLogs.WriteLog("------------------------------------", LogType.Info, "$");
                            foreach (var p in Engine.Plugins)
                            {
                                DebugLogs.WriteLog($" - {p.Key}", LogType.Info, "$");
                            }
                            break;

                        case "plug -info":
                            // Show all inforamtion about a plugin.
                            DebugLogs.WriteLog("What is the name of the plugin ?", LogType.Info, "$");

                            var pName = Console.ReadLine();

                            if (Engine.Plugins.ContainsKey(pName))
                            {
                                var p = Engine.Plugins[pName];
                                DebugLogs.WriteLog("Name : " + pName, LogType.Info, "$");
                                DebugLogs.WriteLog("Version : " + p.GetType().Assembly.GetName().Version, LogType.Info, "$");
                                DebugLogs.WriteLog("Namespace : " + p.GetType().FullName, LogType.Info, "$");
                                DebugLogs.WriteLog("File location : " + p.GetType().Assembly.Location, LogType.Info, "$");
                            }
                            else {

                                DebugLogs.WriteLog("No plugin named : " + pName, LogType.Error, "$");
                            }
                            break;

                        case "content -list":

                            foreach (var item in Content.ContentEngine.ColectionTexture2D)
                            {
                                DebugLogs.WriteLog("Tx2D: " + item.Key, LogType.Info, "$");
                            }
                            foreach (var item in Content.ContentEngine.ColectionFont)
                            {
                                DebugLogs.WriteLog("Font: " + item.Key, LogType.Info, "$");
                            }
                            foreach (var item in Content.ContentEngine.ColectionSong)
                            {
                                DebugLogs.WriteLog("Song: " + item.Key, LogType.Info, "$");
                            }
                            foreach (var item in Content.ContentEngine.ColectionSoundEffect)
                            {
                                DebugLogs.WriteLog("Sound Effect: " + item.Key, LogType.Info, "$");
                            }
                            break;

                        case "debug -gui":
                            Engine.engineConfig.Debug_GuiFrame = true;
                            break;
                        default:

                            DebugLogs.WriteLog("Unknown commande : " + text, LogType.Warning, "$");
                            break;
                    }

                } while (true);
            });
            t = new Thread(GenHandle);

        }

        public void start()
        {
            t.Start();
        }

        public void stop()
        {
            t.Interrupt();
        }

    }
}
