using System;

namespace Maker.RiseEngine.Core.EngineDebug
{
    public static class DebugLogs
    {

        static string LasteDebugText = "";

        /// <summary>
        /// Write something in application logs.
        /// </summary>
        /// <param name="_Text">Text to write in logs.</param>
        /// <param name="_Type">Style of the text.</param>
        /// <param name="_SenderName">Name of the sender modules.</param>
        public static void WriteInLogs(string _Text, LogType _Type = LogType.Info, string _SenderName = "Debug")
        {
            if (Config.Debug.EnableDebugLogs)
            {
                //Getting logs type texte.
                string LogTypeText = "";

                switch (_Type)
                {
                    case LogType.Error:
                        LogTypeText = "Error";
                        break;

                    case LogType.Info:
                        LogTypeText = "Info";
                        break;

                    case LogType.Warning:
                        LogTypeText = "Warning";
                        break;

                    default:
                        LogTypeText = "Info";
                        break;
                }

                //formating text.
                string t = $"{_SenderName} {LogTypeText} {_Text}";

                //write in console.
                Console.WriteLine(t);

                //writing in logs file.
                LasteDebugText = $"{LasteDebugText}{t}{Environment.NewLine}";
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
