namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands
{
    class ClearScreenCommand:IConsoleCommand
    {

        public string Name => "clear";
        public string Description => "Clears the console output";
        public string HelpDocumentation => "clear (no args)";
        private InputProcessor processor;

        public ClearScreenCommand(InputProcessor processor)
        {
            this.processor = processor;
        }

        public string Execute(string[] arguments, EngineConsole console)
        {
            processor.Out.Clear();
            return "";
        }

    }
}
