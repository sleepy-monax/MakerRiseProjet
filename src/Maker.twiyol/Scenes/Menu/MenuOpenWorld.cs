using Maker.RiseEngine.Core;

using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.UserInterface;
using Maker.RiseEngine.Core.UserInterface.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Maker.Twiyol.Scenes.Menu
{
    public class MenuOpenWorld : Scene
    {
        Panel RootContainer;
        Panel controlContainer;
        Label MenuTitle;
        Button goBackButton;
        Button createNewWorldButton;

        public override void OnLoad()
        {

            RootContainer = new Panel(new Rectangle(-350, -250, 700, 500), Color.Transparent);
            RootContainer.Padding = new ControlPadding(16);
            RootContainer.ControlAnchor = Anchor.Center;
            RootContainer.ChildMargin = new ControlPadding(16);

            MenuTitle = new Label("Charger un monde", new Rectangle(0, 0, 64, 64), Color.White);
            MenuTitle.TextStyle = Maker.RiseEngine.Core.Rendering.Style.rectangle;
            MenuTitle.ControlDock = Dock.Top;
            MenuTitle.TextFont = RiseEngine.RESSOUCES.GetSpriteFont("Engine", "Bebas_Neue_48pt");

            controlContainer = new Panel(new Rectangle(0, 0, 0, 96), Color.White);
            controlContainer.Padding = new ControlPadding(16);
            controlContainer.ControlDock = Dock.Bottom;

            goBackButton = new Button("Retour", new Rectangle(0, 0, 200, 64), Color.White);
            goBackButton.ControlDock = Dock.Left;
            goBackButton.onMouseClick += GoBackButton_onMouseClick;

            createNewWorldButton = new Button("Nouveau", new Rectangle(0, 0, 200, 64), Color.White);
            createNewWorldButton.ControlDock = Dock.Right;
            createNewWorldButton.onMouseClick += CreateNewWorldButton_onMouseClick;

            controlContainer.AddChild(goBackButton);
            controlContainer.AddChild(createNewWorldButton);

            RootContainer.AddChild(MenuTitle);
            RootContainer.AddChild(controlContainer);

            foreach (var i in Directory.GetFiles("Saves"))
            {

                Button b = new Button(i, new Rectangle(0, 0, 64, 64), Color.White);
                b.ControlDock = Dock.Top;

                RootContainer.AddChild(b);

            }
        }

        private void CreateNewWorldButton_onMouseClick()
        {
            this.hide();

            Scene scene = new MenuNewWorld();
            RiseEngine.ScenesManager.AddScene(scene);
            scene.show();

            RiseEngine.ScenesManager.RemoveScene(this);
        }

        private void GoBackButton_onMouseClick()
        {
            Scene menu = new Menu.MenuMain();
            RiseEngine.ScenesManager.AddScene(menu);
            menu.show();
            RiseEngine.ScenesManager.RemoveScene(this);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            RootContainer.Draw(spriteBatch, gameTime);
        }


        public override void OnUnload()
        {

        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            RootContainer.Update(playerInput, gameTime);
        }
    }
}
