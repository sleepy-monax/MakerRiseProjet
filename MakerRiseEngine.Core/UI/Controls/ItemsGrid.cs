using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.UI.Controls
{
    public class ItemsGrid : Control
    {

        int ColumnCount;
        int RowCount;
        Rendering.SpriteSheets.Sprite Slot;
        Inventory.ObjInventory inventory;

        public ItemsGrid(int x, int y, int _ColumnCount, int _RowCount, Inventory.ObjInventory _Inventory)
        {

            ColumnCount = _ColumnCount;
            RowCount = _RowCount;
            this.SizeBox = new Rectangle(x, y, _ColumnCount * 64, _RowCount * 64);
            Slot = Rendering.SpriteSheets.CommonSheets.GUI.GetSprite("Slot");
            inventory = _Inventory;

        }

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int cX, int cY)
        {

            for (int Column = 0; Column < ColumnCount; Column++) //X
            {

                for (int Row = 0; Row < RowCount; Row++) //Y
                {

                    int ItemKey = Row * ColumnCount + Column;

                    if (ItemKey < inventory.Slots.Length)
                    {






                        //GameObjectsManager.Items[Inventory.Slots[ItemKey].ID].Variant[Inventory.Slots[ItemKey].Variant].Draw(spriteBatch, new Rectangle(SizeBox.X + cX + Column * 64 + 16, SizeBox.Y + cY + Row * 64 + 16, 32, 32), Color.White, gameTime);

                        if (new Rectangle(SizeBox.X + cX + Column * 64 + 16, SizeBox.Y + cY + Row * 64 + 16, 32, 32).Contains(Common.MouseCursor.MouseLocation))
                        {

                            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                            {

                                Inventory.ObjItem s = inventory.Slots[ItemKey];
                                inventory.Slots[ItemKey] = Common.MouseCursor.Item;

                                Common.MouseCursor.Item = s;
                            }

                        }




                    }

                }

            }

            base.Update(Mouse, KeyBoard, gameTime, cX, cY);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {
            for (int Column = 0; Column < ColumnCount; Column++) //X
            {

                for (int Row = 0; Row < RowCount; Row++) //Y
                {

                    int ItemKey = Row * ColumnCount + Column;

                    if (ItemKey < inventory.Slots.Length)
                    {

                        Slot.Draw(spriteBatch, new Rectangle(SizeBox.X + x + Column * 64, SizeBox.Y + y + Row * 64, 64, 64), Color.White, gameTime);

                        if (inventory.Slots[ItemKey].ID >= 0)
                        {


                            GameObjectsManager.GetGameObject<GameObject.IItem>(inventory.Slots[ItemKey].ID).Variant[inventory.Slots[ItemKey].Variant].Draw(spriteBatch, new Rectangle(SizeBox.X + x + Column * 64 + 16, SizeBox.Y + y + Row * 64 + 16, 32, 32), Color.White, gameTime);
                            spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), inventory.Slots[ItemKey].Count.ToString(), new Rectangle(SizeBox.X + x + Column * 64 + 24, SizeBox.Y + y + Row * 64 + 24, 32, 32), Alignment.Left, Style.DropShadow, Color.White);

                        }

                    }

                }

            }

            base.Draw(spriteBatch, gameTime, x, y);
        }
    }
}
