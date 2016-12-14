using Maker.RiseEngine.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.Scene
{
    public class UItest : Idrawable
    {
        ContainerManager cManager;
        Container container;
        UI.Controls.ItemsGrid Inv;
        UI.Controls.TextBox Txt;

        public UItest() {

            cManager = new ContainerManager();
            container = new Container(new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), true, Dock.UpLeft, Color.White);

            Inventory.ObjInventory ObjI = new Inventory.ObjInventory("test", 64);
            ObjI.Slots[0] = new Inventory.ObjItem(0, 0, 10);

            Inv = new UI.Controls.ItemsGrid(16, 16, 5, 4, ObjI);
            Txt = new UI.Controls.TextBox("Hello", 36, 365, 16);
            container.Controls.Add(Inv);
            container.Controls.Add(Txt);
            cManager.AddContainer("test", container);
            cManager.SwitchContainer("test");
        }

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            cManager.Update(Mouse, KeyBoard, gameTime);
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            cManager.Draw(spriteBatch, gameTime);
        }
    }
}
