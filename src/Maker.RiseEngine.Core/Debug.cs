using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine
{
    public enum LogType { Info, Warning, Error, Message}

    public static class Debug
    {

        public static void WriteLog(string text, LogType logMessageType, string senderName)
        {

            Console.WriteLine($"{logMessageType.ToString()} {senderName} {text}");

        }

    }
}
