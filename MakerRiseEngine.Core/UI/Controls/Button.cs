using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.UI.Controls
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

                switch (mouseStats)
                {
                    case MouseStats.Over:
                        ButM.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + 64 + x, SizeBox.Location.Y + y), new Point(SizeBox.Width - 128, 64)), ControleHoverColor, gameTime);
                        ButL.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x, SizeBox.Location.Y + y), new Point(64)), ControleHoverColor, gameTime);
                        ButR.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x + SizeBox.Width - 64, SizeBox.Location.Y + y), new Point(64)), ControleHoverColor, gameTime);

                        spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(SizeBox.Location.X + x, SizeBox.Location.Y + y - 4, SizeBox.Width, SizeBox.Height), Alignment.Center, Style.Regular, Color.White);

                        break;
                    case MouseStats.Down:
                        ButMD.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + 64 + x, SizeBox.Location.Y + y), new Point(SizeBox.Width - 128, 64)), ControlColor, gameTime);
                        ButLD.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x, SizeBox.Location.Y + y), new Point(64)), ControlColor, gameTime);
                        ButRD.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x + SizeBox.Width - 64, SizeBox.Location.Y + y), new Point(64)), ControlColor, gameTime);

                        spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(SizeBox.Location.X + x, SizeBox.Location.Y + y + 4, SizeBox.Width, SizeBox.Height), Alignment.Center, Style.Regular, Color.White);

                        break;
                    case MouseStats.None:
                        ButM.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + 64 + x, SizeBox.Location.Y + y), new Point(SizeBox.Width - 128, 64)), ControlColor, gameTime);
                        ButL.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x, SizeBox.Location.Y + y), new Point(64)), ControlColor, gameTime);
                        ButR.Draw(spriteBatch, new Rectangle(new Point(SizeBox.Location.X + x + SizeBox.Width - 64, SizeBox.Location.Y + y), new Point(64)), ControlColor, gameTime);

                        spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(SizeBox.Location.X + x, SizeBox.Location.Y + y - 4, SizeBox.Width, SizeBox.Height), Alignment.Center, Style.Regular, Color.White);

                        break;
                    default:
                        break;
                }

            }
            base.Draw(spriteBatch, gameTime, x, y);
        }


    }
}
