using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands;
using System;

namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole
{
    [Serializable]public class GameConsoleOptions
    {
        public int ToggleKey { get; set; }
        public Color BackgroundColor { get; set; }
        public Color FontColor
        {
            set
            {
                BufferColor = PastCommandColor = PastCommandOutputColor = PromptColor = CursorColor = value;
            }
        }
        public Color BufferColor { get; set; }
        public Color PastCommandColor { get; set; }
        public Color PastCommandOutputColor { get; set; }
        public Color PromptColor { get; set; }
        public Color CursorColor { get; set; }
        public float AnimationSpeed { get; set; }
        public float CursorBlinkSpeed { get; set; }
        public int Height { get; set; }
        public string Prompt { get; set; }
        public char Cursor { get; set; }
        public int Padding { get; set; }
        public int Margin { get; set; }
        public bool OpenOnWrite { get; set; }
        public SpriteFont Font { get; set; }

        internal static GameConsoleOptions Options { get; set; } = new GameConsoleOptions();
        internal static List<IConsoleCommand> Commands { get; set; } = new List<IConsoleCommand>();

        public GameConsoleOptions()
        {
            //Default options
            ToggleKey = 44; // F12
            BackgroundColor = new Color(0, 0, 0, 125);
            FontColor = Color.White;
            AnimationSpeed = 0.5f;
            CursorBlinkSpeed = 0.5f;
            Height = 300;
            Prompt = "$";
            Cursor = '_';
            Padding = 0;
            Margin = 0;
            OpenOnWrite = true;
        }

    }
}
