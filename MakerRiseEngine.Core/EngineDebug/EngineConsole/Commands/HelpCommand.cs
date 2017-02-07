using System;
using System.Linq;
using System.Text;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands
{
    class HelpCommand : IConsoleCommand
    {
        public string Name => "help"; 
        public string Description => "Displays the list of commands";
        public string HelpDocumentation => "help <command name>";

        public string Execute(string[] arguments, EngineConsole console)
        {
            if (arguments != null && arguments.Length >= 1)
            {
                var command = GameConsoleOptions.Commands.Where(c => c.Name != null && c.Name == arguments[0]).FirstOrDefault();
                if (command != null)
                {
                    return $"{command.Name}: {command.Description}\n{command.HelpDocumentation}";
                }
                return $"ERROR: Invalid command '{arguments[0]}'";
            }
            var help = new StringBuilder();
            GameConsoleOptions.Commands.Sort(new CommandComparer());
            foreach (var command in GameConsoleOptions.Commands)
            {
                help.Append($"{command.Name}: {command.Description}\n");
            }
            return help.ToString();
        }
    }
}
