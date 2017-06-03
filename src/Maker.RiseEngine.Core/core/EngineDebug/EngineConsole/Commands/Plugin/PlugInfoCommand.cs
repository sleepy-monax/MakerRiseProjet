using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands.Plugin
{
    public class PlugInfoCommand : IConsoleCommand
    {
        public string Name => "plug-info";
        public string Description => "Show information about plugin.";
        public string HelpDocumentation => "plug-info <plugin name>";

        public string Execute(string[] arguments, EngineConsole console)
        {
            var pName = arguments[0];

            if (Rise.Plugins.ContainsKey(pName))
            {
                var output = new StringBuilder();
                var p = Rise.Plugins[pName];
                output.Append("Name : " + pName);
                output.Append("\nVersion : " + p.GetType().Assembly.GetName().Version);
                output.Append("\nNamespace : " + p.GetType().FullName);
                output.Append("\nFile location : " + p.GetType().Assembly.Location);
                return output.ToString();
            }
            else
            {
                return "ERROR: Plugin not found : " + pName;
            }
        }
    }
}
