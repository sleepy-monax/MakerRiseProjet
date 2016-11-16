using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using System.Linq;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.UI.Controls
{
    class TextBox : Control
    {

        public string Text;
        public int MaxChar = 0;
        public int CharIndex = 0;
        KeyboardState PastKeyState = new KeyboardState();

        Sprite TxtM;
        Sprite TxtL;
        Sprite TxtR;

        bool CharInput = false;
        char LastInputChar = '*';

        Vector2 CharSize;

        string VisibleText;


        public TextBox(string text, int maxChar, int X, int Y)
        {
            CharSize = ContentEngine.SpriteFont("Engine", "Consolas_16pt").MeasureString("0");
            this.SizeBox = new Rectangle(X, Y, (int)ContentEngine.SpriteFont("Engine", "Consolas_16pt").MeasureString("0").X * maxChar + 32, 64);
            Text = text;
            MaxChar = maxChar;

            TxtM = CommonSheets.GUI.GetSprite("TxtM");
            TxtL = CommonSheets.GUI.GetSprite("TxtL");
            TxtR = CommonSheets.GUI.GetSprite("TxtR");

            Common.Window.TextInput += Window_TextInput;
        }

        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (!(e.Character == System.Environment.NewLine.ToCharArray()[0]))
            {
                CharInput = true;
                LastInputChar = e.Character;

            }

        }

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int X, int Y)
        {

            if (this.ClickRect.Contains(Mouse.Position))
            {

                //Char Index
                if (KeyBoard.IsKeyDown(Keys.Left)) { if (PastKeyState.IsKeyDown(Keys.Left)) { } else { CharIndex--; } }
                if (KeyBoard.IsKeyDown(Keys.Right)) { if (PastKeyState.IsKeyDown(Keys.Right)) { } else { CharIndex++; } }
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

                PastKeyState = KeyBoard;

                Common.MouseCursor.Type = CursorType.Ibeam;
            }
            else CharInput = false;
            base.Update(Mouse, KeyBoard, gameTime, X, Y);
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
            {
                //Si le nombre de Char est inferieur a la taile de la boite

                VisibleText = Text;

            }
            else
            {


            }

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

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {
            TxtM.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + 64 + x, SizeBox.Location.Y + y), new Point(SizeBox.Width - 128, 64)), Color.White, gameTime);
            TxtL.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x, SizeBox.Location.Y + y), new Point(64)), Color.White, gameTime);
            TxtR.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x + SizeBox.Width - 64, SizeBox.Location.Y + y), new Point(64)), Color.White, gameTime);

            spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), Text, new Rectangle(x + SizeBox.X + 16, y + SizeBox.Y + 4, SizeBox.Width - 32, SizeBox.Height), Alignment.Left, Style.Regular, Color.Black);

            if (this.MouseOver)
                spriteBatch.FillRectangle(new Rectangle((int)ContentEngine.SpriteFont("Engine", "Consolas_16pt").MeasureString(" ").X * CharIndex + x + SizeBox.X + 16, y + SizeBox.Y + 20, 1, (int)CharSize.Y), Color.Black);
            base.Draw(spriteBatch, gameTime, x, y);
        }

    }
}
