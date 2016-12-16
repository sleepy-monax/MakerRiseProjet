using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.UI.Controls
{
    class Slider : Control
    {
        float Value;
        float Width;

        Sprite SldBL = CommonSheets.GUI.GetSprite("SldBL");
        Sprite SldBR = CommonSheets.GUI.GetSprite("SldBR");
        Sprite SldL = CommonSheets.GUI.GetSprite("SldL");
        Sprite SldBM = CommonSheets.GUI.GetSprite("SldBM");
        Sprite SldM = CommonSheets.GUI.GetSprite("SldM");
        Sprite SldOrb = CommonSheets.GUI.GetSprite("SldOrb");

        public Slider(int x, int y, int width) {
            this.SizeBox = new Microsoft.Xna.Framework.Rectangle(x,y,width, 64);
            Width = width;
        }

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int X, int Y)
        {
            if (ClickRect.Contains(Mouse.Position) && Mouse.LeftButton == ButtonState.Pressed) {
                int MousePos = Mouse.Position.X - ClickRect.X;

                if (MousePos == 0) Value = 0;
                else Value = MousePos / Width;
            }

            base.Update(Mouse, KeyBoard, gameTime, X, Y);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {

            //Draw Control background
            spriteBatch.FillRectangle(new Rectangle(ClickRect.X, ClickRect.Y + 16, (int)(ClickRect.Width * Value), ClickRect.Height / 2), new Color(62,115,164));

            SldBL.Draw(spriteBatch, new Rectangle(this.ClickRect.X, this.ClickRect.Y, 64, 64), Color.White, gameTime);
            SldBR.Draw(spriteBatch, new Rectangle(this.ClickRect.X + this.SizeBox.Width - 64, this.ClickRect.Y, 64, 64), Color.White, gameTime);
            SldBM.Draw(spriteBatch, new Rectangle(this.ClickRect.X + 64, this.ClickRect.Y, this.SizeBox.Width - 128, 64), Color.White, gameTime);

            SldL.Draw(spriteBatch, new Rectangle(this.ClickRect.X, this.ClickRect.Y, 64, 64), Color.White, gameTime);
            SldM.Draw(spriteBatch, new Rectangle(this.ClickRect.X + 16, this.ClickRect.Y, (int)(Value * Width) - 16, 64), Color.White, gameTime);
            
            SldOrb.Draw(spriteBatch, new Rectangle((int)(this.ClickRect.X + Value * Width) - 32, this.ClickRect.Y, 64, 64), Color.White, gameTime);

            base.Draw(spriteBatch, gameTime, x, y);
        }

    }
}
