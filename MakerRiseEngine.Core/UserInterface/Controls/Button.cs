using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Maker.RiseEngine.Core.Rendering;

namespace Maker.RiseEngine.Core.UserInterface.Controls
{
    public class Button : Control
    {

        // Sprites.
        // Button idle
        Sprite ButtonMid = CommonSheets.GUI.GetSprite("ButM");
        Sprite ButtonLeft = CommonSheets.GUI.GetSprite("ButL");
        Sprite ButtonRight = CommonSheets.GUI.GetSprite("ButR");

        // Button down.
        Sprite ButtonMidDown = CommonSheets.GUI.GetSprite("ButMD");
        Sprite ButtonLeftDown = CommonSheets.GUI.GetSprite("ButLD");
        Sprite ButtonRightDown = CommonSheets.GUI.GetSprite("ButRD");

        public Button(string text, Rectangle rect, Color color) {

            Text = text;
            ControlRectangle = rect;
            ControlColor = color;
            TextColor = Color.Black;

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (mouseStats == MouseStats.Over || mouseStats == MouseStats.None)
            {

                // Draw button body.
                DrawSprite(spriteBatch, ButtonLeft, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
                DrawSprite(spriteBatch, ButtonMid, new Rectangle(64, 0, ControlRectangle.Width - 128, 64), ControlColor, gameTime);
                DrawSprite(spriteBatch, ButtonRight, new Rectangle(ControlRectangle.Width - 64, 0, 64, 64), ControlColor, gameTime);

                // Over animation
                if (mouseStats == MouseStats.Over)
                {

                }

                // Draw text.
                DrawText(spriteBatch, ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text,
                    // Placing.
                    new Rectangle(0,0, ControlRectangle.Width, ControlRectangle.Height),
                    // Style.
                    TextColor, SpriteFontDraw.Alignment.Center, SpriteFontDraw.Style.Regular);

            }
            else {

                // Button down animation.

                // Draw button body.
                DrawSprite(spriteBatch, ButtonLeftDown, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
                DrawSprite(spriteBatch, ButtonMidDown, new Rectangle(64, 0, ControlRectangle.Width - 128, 64), ControlColor, gameTime);
                DrawSprite(spriteBatch, ButtonRightDown, new Rectangle(ControlRectangle.Width - 64, 0, 64, 64), ControlColor, gameTime);

                // Draw text.
                // Draw text.
                DrawText(spriteBatch, ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Text,
                    // Placing.
                    new Rectangle(0, 0, ControlRectangle.Width, ControlRectangle.Height),
                    // Style.
                    TextColor, SpriteFontDraw.Alignment.Center, SpriteFontDraw.Style.Regular);

            }


        }

    }
}
