using System;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands
{
    class CustomCommand:IConsoleCommand
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string HelpDocumentation { get; private set; }

        private Func<string[], string> action;

        public CustomCommand(string name, Func<string[], string> action, string description, string helpDocumentation)
        {
            Name = name;
            Description = description;
            HelpDocumentation = helpDocumentation;
            this.action = action;
        }
        public string Execute(string[] arguments, EngineConsole console)
        {
            return action(arguments);
        }
    }
}
