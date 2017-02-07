using System;

namespace Maker.RiseEngine.Core.EngineDebug
{
    public static class DebugLogs
    {

        static string LastDebugText = "";

        /// <summary>
        /// Write something in application logs.
        /// </summary>
        /// <param name="_Text">Text to write in logs.</param>
        /// <param name="_Type">Style of the text.</param>
        /// <param name="_SenderName">Name of the sender modules.</param>
        public static void WriteLog(string _Text, LogType _Type = LogType.Info, string _SenderName = "Debug")
        {
            if (Engine.engineConfig.Debug_EnableLogs)
            {
                //Getting logs type texte.
                string LogTypeText = "";

                switch (_Type)
                {
                    case LogType.Error:
                        System.Console.ForegroundColor = ConsoleColor.DarkRed;
                        LogTypeText = "E!";
                        break;

                    case LogType.Info:
                        System.Console.ForegroundColor = ConsoleColor.White;
                        LogTypeText = "I:";
                        break;

                    case LogType.Warning:
                        System.Console.ForegroundColor = ConsoleColor.Yellow;
                        LogTypeText = "W?";
                        break;

                    default:
                        System.Console.ForegroundColor = ConsoleColor.White;
                        LogTypeText = "I:";
                        break;
                }

                //formating text.
                string t = $"{LogTypeText} {_SenderName} {_Text}";

                //write in console.
                System.Console.WriteLine(t);

                //writing in logs file.
                LastDebugText = $"{LastDebugText}{t}{Environment.NewLine}";
                System.Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
    public enum LogType
    {
        Error,
        Info,
        Warning,
    }
}
