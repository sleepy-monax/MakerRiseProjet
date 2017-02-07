using Microsoft.Xna.Framework;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands
{
    class ExitCommand : IConsoleCommand
    {
        public string Name => "exit";
        public string Description => "Forcefully exists the game";
        public string HelpDocumentation => "exit (no args)";

        private readonly Game game;
        public ExitCommand(Game game)
        {
            this.game = game;
        }
        public string Execute(string[] arguments, EngineConsole console)
        {
            game.Exit();
            return "Exiting the game";
        }
    }
}