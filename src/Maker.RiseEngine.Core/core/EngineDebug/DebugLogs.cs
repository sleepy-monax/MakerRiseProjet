using System;

namespace Maker.RiseEngine.Core.EngineDebug
{
    public static class DebugLogs
    {

        static string LastDebugText = "";

        /// <summary>
        /// Write something in application logs.
        /// </summary>
        /// <param name="message">Text to write in logs.</param>
        /// <param name="messageType">Style of the text.</param>
        /// <param name="senderName">Name of the sender modules.</param>
        public static void WriteLog(string message, LogType messageType = LogType.Info, string senderName = "Debug")
        {
            if (Rise.Engine.userConfig.DebugEnableLogs)
            {
                switch (messageType)
                {
                    case LogType.Error:
                        System.Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;

                    case LogType.Info:
                        System.Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case LogType.Warning:
                        System.Console.ForegroundColor = ConsoleColor.Yellow;
                        break;

                    default:
                        System.Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                //formating text.
                string t = $"{messageType} {senderName} {message}";

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
