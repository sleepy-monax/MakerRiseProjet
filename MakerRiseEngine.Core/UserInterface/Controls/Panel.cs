using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;

namespace Maker.RiseEngine.Core.UserInterface.Controls
{
    public class Panel : Control
    {

        Sprite panelCenter = CommonSheets.GUI.GetSprite("BoxC");

        Sprite panelUpLeft = CommonSheets.GUI.GetSprite("BoxUL");
        Sprite panelDownLeft = CommonSheets.GUI.GetSprite("BoxDL");

        Sprite panelUpRight = CommonSheets.GUI.GetSprite("BoxUR");
        Sprite panelDownRight = CommonSheets.GUI.GetSprite("BoxDR");

        Sprite panelMidUp = CommonSheets.GUI.GetSprite("BoxMU");
        Sprite panelMidDown = CommonSheets.GUI.GetSprite("BoxMD");
        Sprite panelMidLeft = CommonSheets.GUI.GetSprite("BoxML");
        Sprite panelMidRight = CommonSheets.GUI.GetSprite("BoxMR");

        public Panel(Rectangle rect, Color color)
        {
            ControlRectangle = rect;
            ControlColor = color;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw center.
            DrawSprite(spriteBatch, panelCenter, new Rectangle(new Point(64), ControlRectangle.Size - new Point(128)), ControlColor, gameTime);

            // Draw corners.
            DrawSprite(spriteBatch, panelUpLeft, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelDownLeft, new Rectangle(0, ControlRectangle.Height - 64, 64, 64), ControlColor, gameTime);

            DrawSprite(spriteBatch, panelUpRight, new Rectangle(ControlRectangle.Width - 64, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelDownRight, new Rectangle(ControlRectangle.Width - 64, ControlRectangle.Height - 64, 64, 64), ControlColor, gameTime);

            // Draw edges.
            DrawSprite(spriteBatch, panelMidLeft, new Rectangle(0, 64, 64, ControlRectangle.Height - 128), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelMidRight, new Rectangle(ControlRectangle.Width - 64, 64, 64, ControlRectangle.Height - 128), ControlColor, gameTime);

            DrawSprite(spriteBatch, panelMidUp, new Rectangle(64, 0, ControlRectangle.Width - 128, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelMidDown, new Rectangle(64, ControlRectangle.Height - 64, ControlRectangle.Width, 64), ControlColor, gameTime);
        }

    }
}
