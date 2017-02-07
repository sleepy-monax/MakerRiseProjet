using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands.Plugin
{
    public class PlugCommand : IConsoleCommand
    {
        public string Name => "plug";
        public string Description => "Get list and info of plugins.";
        public string HelpDocumentation => "plug<-list|-info>";

        public string Execute(string[] arguments, EngineConsole console)
        {
            return "plug<-list|-info>";
        }
    }
}
