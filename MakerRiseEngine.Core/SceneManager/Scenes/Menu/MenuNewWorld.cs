using Maker.RiseEngine.Core.UserInterface.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.SceneManager.Scenes.Menu
{
    public class MenuNewWorld : Scene
    {
        Panel rootContainer;
        Panel controlContainer;

        Button createNewWorldButton;
        Button goBackButton;

        TextBox worldNameTexBox;
        TextBox worldSeedTextBox;

        


        public override void OnLoad()
        {

            rootContainer = new Panel(new Rectangle(-350, -250, 700, 500), Color.White);
            rootContainer.Padding = new UserInterface.ControlPadding(16);
            rootContainer.ControlAnchor = UserInterface.Anchor.Center;

            controlContainer = new Panel(new Rectangle(0, 0, 0, 96), Color.White);
            controlContainer.Padding = new UserInterface.ControlPadding(16);
            controlContainer.ControlDock = UserInterface.Dock.Down;

            createNewWorldButton = new Button("Créer le nouveau monde", new Rectangle(0, 0, 400, 64), Color.White);
            createNewWorldButton.ControlDock = UserInterface.Dock.Right;

            goBackButton = new Button("Retour", new Rectangle(0, 0, 200, 64), Color.White);
            goBackButton.ControlDock = UserInterface.Dock.Left;
            goBackButton.onMouseClick += GoBackButton_onMouseClick;



            rootContainer.AddChild(controlContainer);

            controlContainer.AddChild(createNewWorldButton);
            controlContainer.AddChild(goBackButton);
        }

        private void GoBackButton_onMouseClick()
        {
            Scene menu = new Menu.MenuMain();
            Game.sceneManager.AddScene(menu);
            menu.show();
            Game.sceneManager.RemoveScene(this);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            rootContainer.Draw(spriteBatch, gameTime);

        }

        public override void OnUnload()
        {



        }

        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {

            rootContainer.Update(mouse, keyBoard, gameTime);

        }

    }
}
