using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RiseEngine.Core.Rendering.SpriteSheets;

namespace RiseEngine.Core.UI.Controls
{
    public class Button : Control
    {

        public string Text = "Button";
        Sprite ButM = CommonSheets.GUI.GetSprite("ButM");
        Sprite ButL = CommonSheets.GUI.GetSprite("ButL");
        Sprite ButR = CommonSheets.GUI.GetSprite("ButR");

        Sprite ButMD = CommonSheets.GUI.GetSprite("ButMD");
        Sprite ButLD = CommonSheets.GUI.GetSprite("ButLD");
        Sprite ButRD = CommonSheets.GUI.GetSprite("ButRD");

        Color ControlColor;
        Color ControleHoverColor;

        bool Visible = true;

        public Button(string text, int Width, int x, int y, Color _color)
        {
            Text = text;
            this.SizeBox.Height = 64;
            SizeBox.Width = Width;
            SizeBox.X = x;
            SizeBox.Y = y;
            ControlColor = _color;
            ControleHoverColor = new Color((int)(ControlColor.R * 0.9), (int)(ControlColor.G * 0.9), (int)(ControlColor.B * 0.9));
        }

        public Button(string text, int Width, int x, int y, Color _color, bool _visible)
        {
            Text = text;
            this.SizeBox.Height = 64;
            SizeBox.Width = Width;
            SizeBox.X = x;
            SizeBox.Y = y;
            ControlColor = _color;
            ControleHoverColor = new Color((int)(ControlColor.R * 0.9), (int)(ControlColor.G * 0.9), (int)(ControlColor.B * 0.9));
            Visible = _visible;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {
            if (Visible)
            {

                if (this.MouseDown)
                {
                    ButMD.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + 64 + x, SizeBox.Location.Y + y), new Point(SizeBox.Width - 128, 64)), ControlColor, gameTime);
                    ButLD.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x, SizeBox.Location.Y + y), new Point(64)), ControlColor, gameTime);
                    ButRD.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x + SizeBox.Width - 64, SizeBox.Location.Y + y), new Point(64)), ControlColor, gameTime);

                    helper.DrawString(spriteBatch, ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(SizeBox.Location.X + x, SizeBox.Location.Y + y + 4, SizeBox.Width, SizeBox.Height), helper.Alignment.Center, helper.Style.Regular, Color.White);
                }
                else if (this.MouseOver)
                {
                    ButM.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + 64 + x, SizeBox.Location.Y + y), new Point(SizeBox.Width - 128, 64)), ControleHoverColor, gameTime);
                    ButL.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x, SizeBox.Location.Y + y), new Point(64)), ControleHoverColor, gameTime);
                    ButR.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x + SizeBox.Width - 64, SizeBox.Location.Y + y), new Point(64)), ControleHoverColor, gameTime);

                    helper.DrawString(spriteBatch, ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(SizeBox.Location.X + x, SizeBox.Location.Y + y - 4, SizeBox.Width, SizeBox.Height), helper.Alignment.Center, helper.Style.Regular, Color.White);
                }
                else
                {
                    ButM.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + 64 + x, SizeBox.Location.Y + y), new Point(SizeBox.Width - 128, 64)), ControlColor, gameTime);
                    ButL.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x, SizeBox.Location.Y + y), new Point(64)), ControlColor, gameTime);
                    ButR.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x + SizeBox.Width - 64, SizeBox.Location.Y + y), new Point(64)), ControlColor, gameTime);

                    helper.DrawString(spriteBatch, ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(SizeBox.Location.X + x, SizeBox.Location.Y + y - 4, SizeBox.Width, SizeBox.Height), helper.Alignment.Center, helper.Style.Regular, Color.White);
                }
            }



            
            base.Draw(spriteBatch, gameTime, x, y);
        }


    }
}
