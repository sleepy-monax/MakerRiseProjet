using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands;
using Maker.RiseEngine.Core.Input;
using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole
{
    public class EngineConsole : IDrawable
    {
        public bool IsOpen
        {
            get
            {
                return Renderer.IsOpen;
            }
        }
        private readonly InputProcessor Input;
        private readonly Renderer Renderer;
        public GameConsoleOptions Options { get { return GameConsoleOptions.Options; } }
        public List<IConsoleCommand> Commands { get { return GameConsoleOptions.Commands; } }
        SpriteBatch sb;

        public EngineConsole(SpriteBatch spriteBatch, GameConsoleOptions options, GameEngine game)
        {
            if (options.Font == null)
                throw new NullReferenceException("Please, provide SpriteFont for console font!");

            GameConsoleOptions.Options = options;
            sb = spriteBatch;

            Input = new InputProcessor(new CommandProcesser(this), game.Window);
            Renderer = new Renderer(game, Input);
           
            Input.Open += (s, e) => Renderer.Open();
            Input.Close += (s, e) => Renderer.Close();


            var inbuiltCommands = new IConsoleCommand[] { new ClearScreenCommand(Input), new ExitCommand(game), new HelpCommand() };
            GameConsoleOptions.Commands.AddRange(inbuiltCommands);
        }

        /// <summary>
        /// Write directly to the output stream of the console
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
            Input.AddToOutput(text);
        }

        public void Update(GameInput playerInput, GameTime gameTime)
        {
            Renderer.Update(playerInput,gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sb.Begin();
            Renderer.Draw(spriteBatch,gameTime);
            sb.End();
        }


        /// <summary>
        /// Adds a new command to the console
        /// </summary>
        /// <param name="commands"></param>
        public void AddCommand(params IConsoleCommand[] commands)
        {
            Commands.AddRange(commands);
        }

        /// <summary>
        /// Adds a new command to the console
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="action"></param>
        public void AddCommand(string name, Func<string[], string> action)
        {
            AddCommand(name, action, "", "");
        }

        /// <summary>
        /// Adds a new command to the console
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        public void AddCommand(string name, Func<string[], string> action, string description, string helpDocumentation)
        {
            Commands.Add(new CustomCommand(name, action, description, helpDocumentation));
        }
    }
}
