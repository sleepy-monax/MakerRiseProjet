using Maker.RiseEngine.Core.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core.Input;

using Maker.Twiyol.Inventory;
using Maker.RiseEngine.Core;

namespace Maker.Twiyol.Game.GameUI
{
    public class ItemSlot : Control
    {
        DataInventory Inventory;
        int ItemIndex;
        Texture2D slotT2D = Rise.Engine.ressourceManager.GetTexture2D("twiyol", "Item_Slot");

        public ItemSlot(Point location, DataInventory inventory, int itemIndex) {
            Inventory = inventory;
            ItemIndex = itemIndex;
            this.Bound = new Rectangle(location, new Point(64));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawT2D(spriteBatch, slotT2D, new Rectangle(0, 0, 64, 64), Color.White);
            if (Inventory.Slots[ItemIndex] != null) {

                DrawSprite(spriteBatch, Inventory.Slots[ItemIndex].ToGameObject().ItemSprite , new Rectangle(0, 0, 64, 64), Color.White, gameTime);

            }
        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            
        }
    }
}
