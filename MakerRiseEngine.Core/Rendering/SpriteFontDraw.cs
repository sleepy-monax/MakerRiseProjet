using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.RiseEngine.Core.Rendering
{
    public static class SpriteFontDraw
    {

        public enum Alignment { Center = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 }

        public enum Style { Regular = 0, Bold = 1, DropShadow = 2, rectangle = 3}

        public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle bounds, Alignment align, Style style, Color color)
        {

            // Custom text drawing
            Vector2 size = font.MeasureString(text);
            Vector2 pos = bounds.Center.ToVector2();
            Vector2 origin = size / 2;

            // Text align.
            if (align.HasFlag(Alignment.Left))
                origin.X += bounds.Width / 2 - size.X / 2;

            if (align.HasFlag(Alignment.Right))
                origin.X -= bounds.Width / 2 - size.X / 2;

            if (align.HasFlag(Alignment.Top))
                origin.Y += bounds.Height / 2 - size.Y / 2;

            if (align.HasFlag(Alignment.Bottom))
                origin.Y -= bounds.Height / 2 - size.Y / 2;

            // Text style.

            switch (style)
            {
                case Style.Regular:
                    break;
                case Style.Bold:
                    spriteBatch.DrawString(font, text, new Vector2(pos.X + 1, pos.Y + 1), color, 0, origin, 1, SpriteEffects.None, 0);
                    break;
                case Style.DropShadow:
                    spriteBatch.DrawString(font, text, new Vector2(pos.X + 2, pos.Y + 2), new Color(0, 0, 0, 125), 0, origin, 1, SpriteEffects.None, 0);
                    break;
                case Style.rectangle:

                    Vector2 textSize = font.MeasureString(text);
                    spriteBatch.FillRectangle(new Rectangle((pos - origin - new Vector2(4)).ToPoint(), new Point((int)textSize.X, (int)textSize.Y) + new Point(8)) , Color.Black);

                    break;
                default:
                    break;
            }

            spriteBatch.DrawString(font, text, pos - origin, color);
        }

    }
}
