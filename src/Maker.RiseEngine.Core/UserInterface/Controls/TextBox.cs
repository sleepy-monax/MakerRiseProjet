using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Linq;

namespace Maker.RiseEngine.Core.UserInterface.Controls
{
    public class TextBox : Control
    {
        public int MaxChar = 0;
        public int CharIndex;

        Sprite TxtM;
        Sprite TxtL;
        Sprite TxtR;

        bool CharInput = false;
        char LastInputChar = '*';

        Vector2 CharSize;

        string VisibleText;

        public TextBox(string text, Rectangle rect, Color color, Color textColor)
        {
            CharSize = rise.ENGINE.RESSOUCES.SpriteFont("Engine", "Consolas_16pt").MeasureString("_");
            Text = text;
            this.ControlRectangle = rect;
            ControlColor = color;
            TextColor = textColor;
            MaxChar = (int)(rect.Width / CharSize.X);

            TxtM = CommonSheets.GUI.GetSprite("TxtM");
            TxtL = CommonSheets.GUI.GetSprite("TxtL");
            TxtR = CommonSheets.GUI.GetSprite("TxtR");

            CharIndex = text.Length;

            rise.Window.TextInput += Window_TextInput;
        }

        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            // handel text input event.
            if (!(e.Character == System.Environment.NewLine.ToCharArray()[0]))
            {
                CharInput = true;
                LastInputChar = e.Character;
            }
        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {

            if (this.mouseStats == MouseStats.Over)
            {

                //Char Index
                if (playerInput.IsKeyBoardKeyPress(Keys.Left)) { CharIndex--;  }
                if (playerInput.IsKeyBoardKeyPress(Keys.Right)) { CharIndex++;  }
                if (CharIndex < 0) { CharIndex = 0; }
                if (CharIndex > Text.ToCharArray().Count()) { CharIndex = Text.ToCharArray().Count(); }

                //BackSpace
                if (CharInput == true)
                {
                    CharInput = false;

                    if (LastInputChar == (char)8)
                        RemoveChar();
                    else
                        InputChar(LastInputChar);
                }

            }
            else CharInput = false;
        }


        public void InputChar(char chr)
        {
            if (Text.ToCharArray().Count() < MaxChar)
            {
                string CharString = chr.ToString();
                Text = Text.Insert(CharIndex, CharString);
                CharIndex++;
            }
        }

        public void RefreshText()
        {
            if (Text.ToCharArray().Count() < MaxChar)
                VisibleText = Text;
        }

        public void RemoveChar()
        {
            if (Text.ToCharArray().Count() > 0)
            {
                int RemoveIndex = CharIndex - 1;
                if (RemoveIndex < 0) { RemoveIndex = 0; }
                Text = Text.Remove(RemoveIndex, 1);
                CharIndex--;
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw textbox background.
            DrawSprite(spriteBatch, TxtL, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, TxtM, new Rectangle(64, 0, ControlRectangle.Width - 128, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, TxtR, new Rectangle(ControlRectangle.Width - 64, 0, 64, 64), ControlColor, gameTime);


            // Draw text.
            DrawText(spriteBatch, rise.ENGINE.RESSOUCES.SpriteFont("Engine", "Consolas_16pt"), Text, new Rectangle(16, 0, ControlRectangle.Width, ControlRectangle.Height), TextColor, Alignment.Left, Style.Regular);

            // Draw selection.
            if (mouseStats == MouseStats.Over)
                spriteBatch.FillRectangle(new Rectangle((int)rise.ENGINE.RESSOUCES.SpriteFont("Engine", "Consolas_16pt").MeasureString(" ").X * CharIndex + ControlRectangle.X + 16, ControlRectangle.Y + 16, 1, (int)CharSize.Y), Color.Black);

        }


    }
}
