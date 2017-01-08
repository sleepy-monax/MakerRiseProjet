using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.EngineDebug
{
    public class debugTerminal
    {

        public void start() {
            ThreadStart GenHandle = new ThreadStart(delegate
            {
                do
                {
                    var text = Console.ReadLine();
                    DebugLogs.WriteLog(text, LogType.Info, "$");
                } while (true);
            });
            Thread t = new Thread(GenHandle);
            t.Start();
        }

    }
}
