using Maker.RiseEngine.Core;

using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.Storage;
using Maker.RiseEngine.Core.UserInterface;
using Maker.RiseEngine.Core.UserInterface.Controls;
using Maker.Twiyol.Game.WorldDataStruct;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Threading;

namespace Maker.Twiyol.Scenes.Menu
{
    public class MenuOpenWorld : Scene
    {
        Panel RootContainer;
        Panel controlContainer;
        Label MenuTitle;
        Button goBackButton;
        Button createNewWorldButton;
        TextBox WorldNameTextBox;

        public override void OnLoad()
        {

            RootContainer = new Panel(new Rectangle(-350, -250, 700, 500), Color.Transparent);
            RootContainer.Padding = new ControlPadding(16);
            RootContainer.Anchor = Anchors.Center;
            RootContainer.ChildMargin = new ControlPadding(16);

            MenuTitle = new Label("Charger un monde", new Rectangle(0, 0, 64, 64), Color.White);
            MenuTitle.TextStyle = Maker.RiseEngine.Core.Rendering.Style.rectangle;
            MenuTitle.Dock = Docks.Top;
            MenuTitle.TextFont = Engine.ressourceManager.GetSpriteFont("Engine", "Bebas_Neue_48pt");

            controlContainer = new Panel(new Rectangle(0, 0, 0, 96), Color.White);
            controlContainer.Padding = new ControlPadding(16);
            controlContainer.Dock = Docks.Bottom;

            goBackButton = new Button("Retour", new Rectangle(0, 0, 200, 64), Color.White);
            goBackButton.Dock = Docks.Left;
            goBackButton.MouseClick += GoBackButton_onMouseClick;

            createNewWorldButton = new Button("Nouveau", new Rectangle(0, 0, 200, 64), Color.White);
            createNewWorldButton.Dock = Docks.Right;
            createNewWorldButton.MouseClick += CreateNewWorldButton_onMouseClick;

            controlContainer.AddChild(goBackButton);
            controlContainer.AddChild(createNewWorldButton);

            RootContainer.AddChild(MenuTitle);
            RootContainer.AddChild(controlContainer);

            WorldNameTextBox = new TextBox("", new Rectangle(0, 0, 200, 64), Color.White, Color.Black);
            WorldNameTextBox.Dock = Docks.Top;
            RootContainer.AddChild(WorldNameTextBox);
            Button ButtonLoadWorld = new Button("Load World", new Rectangle(0, 0, 64, 64), Color.White);
            ButtonLoadWorld.Dock = Docks.Top;
            ButtonLoadWorld.MouseClick += B_MouseClick;

            RootContainer.AddChild(ButtonLoadWorld);
            return;

            foreach (var i in Directory.GetFiles("Saves"))
            {

                Button b = new Button(i, new Rectangle(0, 0, 64, 64), Color.White);
                b.Dock = Docks.Top;

                RootContainer.AddChild(b);

            }
        }

        private void B_MouseClick()
        {
            ThreadStart GenHandle = new ThreadStart(delegate
            {
                Engine.sceneManager.RemoveScene(this);

                var world = SerializationHelper.LoadFromBin<DataWorld>($"Saves/{WorldNameTextBox.Text}.bin");
                Game.GameScene wrldsc = new Game.GameScene(world);

                Engine.sceneManager.AddScene(wrldsc);

                wrldsc.Show();
            });

            Thread t = new Thread(GenHandle);
            t.Start();
        }

        private void CreateNewWorldButton_onMouseClick()
        {
            this.hide();

            Scene scene = new MenuNewWorld();
            Engine.sceneManager.AddScene(scene);
            scene.Show();

            Engine.sceneManager.RemoveScene(this);
        }

        private void GoBackButton_onMouseClick()
        {
            Scene menu = new Menu.MainMenu();
            Engine.sceneManager.AddScene(menu);
            menu.Show();
            Engine.sceneManager.RemoveScene(this);
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
