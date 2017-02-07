using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands.Plugin
{
    class PlugListCommand : IConsoleCommand
    {
        public string Name => "plug-list";
        public string Description => "Show a list of all loaded plugins.";
        public string HelpDocumentation => "plug-list (no args)";

        public string Execute(string[] arguments, EngineConsole console)
        {
            var output = new StringBuilder();

            output.Append("This is the list of loaded plugins :\n");
            output.Append("------------------------------------\n");

            foreach (var p in Engine.Plugins)
            {

                output.Append($" - {p.Key}\n");
            }

            return output.ToString();
        }
    }
}
