using Maker.RiseEngine.Rendering.SpriteSheets;
using Maker.RiseEngine.Ressources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.UserInterface.Controls
{
    public class Panel : Control
    {

        Sprite panelCenter = Common.UserInterface.GetSprite("BoxC");

        Sprite panelUpLeft = Common.UserInterface.GetSprite("BoxUL");
        Sprite panelDownLeft = Common.UserInterface.GetSprite("BoxDL");

        Sprite panelUpRight = Common.UserInterface.GetSprite("BoxUR");
        Sprite panelDownRight = Common.UserInterface.GetSprite("BoxDR");

        Sprite panelMidUp = Common.UserInterface.GetSprite("BoxMU");
        Sprite panelMidDown = Common.UserInterface.GetSprite("BoxMD");
        Sprite panelMidLeft = Common.UserInterface.GetSprite("BoxML");
        Sprite panelMidRight = Common.UserInterface.GetSprite("BoxMR");

        public Panel(Rectangle rect, Color color)
        {
            Bound = rect;
            ControlColor = color;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw center.
            DrawSprite(spriteBatch, panelCenter, new Rectangle(new Point(64), Bound.Size - new Point(128)), ControlColor, gameTime);

            // Draw corners.
            DrawSprite(spriteBatch, panelUpLeft, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelDownLeft, new Rectangle(0, Bound.Height - 64, 64, 64), ControlColor, gameTime);

            DrawSprite(spriteBatch, panelUpRight, new Rectangle(Bound.Width - 64, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelDownRight, new Rectangle(Bound.Width - 64, Bound.Height - 64, 64, 64), ControlColor, gameTime);

            // Draw edges.
            DrawSprite(spriteBatch, panelMidLeft, new Rectangle(0, 64, 64, Bound.Height - 128), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelMidRight, new Rectangle(Bound.Width - 64, 64, 64, Bound.Height - 128), ControlColor, gameTime);

            DrawSprite(spriteBatch, panelMidUp, new Rectangle(64, 0, Bound.Width - 128, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelMidDown, new Rectangle(64, Bound.Height - 64, Bound.Width - 128, 64), ControlColor, gameTime);
        }

    }
}
