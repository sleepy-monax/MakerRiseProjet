using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiseEngine.Core.Rendering.SpriteSheets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static RiseEngine.Core.Rendering.SpriteFontDraw;

namespace RiseEngine.Core.UI.Controls
{
    public class CheckBox : Control
    {

        //Sprites 
        Sprite CheckV;
        Sprite CheckB;

        public string Text;
        public bool IsChecked;

        public CheckBox(string _Text, bool _Checked, int Width, int x, int y)
        {
            CheckV = CommonSheets.GUI.GetSprite("CheckV");
            CheckB = CommonSheets.GUI.GetSprite("CheckB");

            Text = _Text;
            IsChecked = _Checked;

            SizeBox.Height = 64;
            SizeBox.Width = Width;
            SizeBox.X = x;
            SizeBox.Y = y;

            this.OnMouseClick += new UI.Control.ClickEventHandler(this.CheckBox_MouseClick);
        }

        private void CheckBox_MouseClick()
        {
            IsChecked = !IsChecked;
        }

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int ConatinerX, int ContainerY)
        {
            base.Update(Mouse, KeyBoard, gameTime, ConatinerX, ContainerY);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {

            if (IsChecked)
            {
                CheckV.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x - 2, SizeBox.Location.Y + y), new Point(64)), Color.White, gameTime);

            }
            else
            {
                CheckB.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x - 2, SizeBox.Location.Y + y), new Point(64)), Color.White, gameTime);
            }

            spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(SizeBox.Location.X + x + 64, SizeBox.Location.Y + y, SizeBox.Width - 64, SizeBox.Height), Alignment.Left, Style.Regular, Color.White);

            base.Draw(spriteBatch, gameTime, x, y);
        }

    }
}
