using System.Collections.Generic;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands
{
    class CommandComparer:IComparer<IConsoleCommand>
    {
        public int Compare(IConsoleCommand x, IConsoleCommand y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
