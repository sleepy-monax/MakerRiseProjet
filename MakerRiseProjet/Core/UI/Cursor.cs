using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RiseEngine.Core.Rendering.SpriteSheets;
using RiseEngine.Core.Inventory;
using static RiseEngine.Core.Rendering.SpriteFontDraw;

namespace RiseEngine.Core.UI
{
    public enum CursorType {
        Cursor,Ibeam, Hiden
    }

    public class Cursor : Idrawable
    {

        Sprite sCursor;
        Sprite sIBeam;
        public CursorType Type = CursorType.Cursor;

        public Inventory.ObjItem Item = new ObjItem(-1, 0, 0);

        public Cursor() {

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
                    sCursor.Draw(spriteBatch, new Rectangle(MouseLocation + new Point(3,2), new Point(32)), new Color(0,0,0,100), gameTime);
                    sCursor.Draw(spriteBatch, new Rectangle(MouseLocation, new Point(32)), Color.White, gameTime);
                    break;
                case CursorType.Ibeam:
                    sIBeam.Draw(spriteBatch, new Rectangle(MouseLocation, new Point(32)), Color.White, gameTime);
                    break;
                default:
                    sCursor.Draw(spriteBatch, new Rectangle(MouseLocation, new Point(32)), Color.White, gameTime);
                    break;
            }

            if (!(Item.ID == -1)) {
                GameObjectsManager.Items[Item.ID].Variant[Item.Variant].Draw(spriteBatch, new Rectangle(MouseLocation + new Point(8,22), new Point(32)), Color.White, gameTime);
                spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Item.Count.ToString(), new Rectangle(MouseLocation + new Point(16,30), new Point(32)), Alignment.Left, Style.DropShadow, Color.White);
            }
        }

    }
}
