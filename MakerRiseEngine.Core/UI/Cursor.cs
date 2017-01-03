using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Inventory;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.UI
{
    public enum CursorType
    {
        Cursor, Ibeam, Hiden
    }

    public class Cursor : Idrawable
    {

        Sprite sCursor;
        Sprite sIBeam;
        public CursorType Type = CursorType.Cursor;

        public Cursor()
        {

            sCursor = CommonSheets.Cursor.GetSprite("Cursor");
            sIBeam = CommonSheets.Cursor.GetSprite("IBeam");
        }

        public Point MouseLocation = Point.Zero;

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            Type = CursorType.Cursor;
            MouseLocation = Mouse.Position;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (Type)
            {
                case CursorType.Cursor:
                    sCursor.Draw(spriteBatch, new Rectangle(MouseLocation + new Point(3, 2), new Point(32)), new Color(0, 0, 0, 100), gameTime);
                    sCursor.Draw(spriteBatch, new Rectangle(MouseLocation, new Point(32)), Color.White, gameTime);
                    break;
                case CursorType.Ibeam:
                    sIBeam.Draw(spriteBatch, new Rectangle(MouseLocation, new Point(32)), Color.White, gameTime);
                    break;
                default:
                    sCursor.Draw(spriteBatch, new Rectangle(MouseLocation, new Point(32)), Color.White, gameTime);
                    break;
            }

        }

    }
}
