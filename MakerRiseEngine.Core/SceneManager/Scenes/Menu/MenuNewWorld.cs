using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.UserInterface.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

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

        Label titleLabel;
        Label nameLabel;
        Label seedLabel;
        
        public override void OnLoad()
        {

            rootContainer = new Panel(new Rectangle(-350, -250, 700, 500), Color.Transparent);
            rootContainer.Padding = new UserInterface.ControlPadding(16);
            rootContainer.ControlAnchor = UserInterface.Anchor.Center;

            controlContainer = new Panel(new Rectangle(0, 0, 0, 96), Color.White);
            controlContainer.Padding = new UserInterface.ControlPadding(16);
            controlContainer.ControlDock = UserInterface.Dock.Bottom;

            createNewWorldButton = new Button("Créer le nouveau monde", new Rectangle(0, 0, 400, 64), Color.White);
            createNewWorldButton.ControlDock = UserInterface.Dock.Right;
            createNewWorldButton.onMouseClick += CreateNewWorldButton_onMouseClick;

            goBackButton = new Button("Retour", new Rectangle(0, 0, 200, 64), Color.White);
            goBackButton.ControlDock = UserInterface.Dock.Left;
            goBackButton.onMouseClick += GoBackButton_onMouseClick;

            worldNameTexBox = new TextBox("Monde sans nom", new Rectangle(0, 0, 128, 64), Color.White, Color.Black);
            worldNameTexBox.ControlDock = UserInterface.Dock.Top;
            worldSeedTextBox = new TextBox(new Random().Next().ToString(), new Rectangle(0, 0, 128, 64), Color.White, Color.Black);
            worldSeedTextBox.ControlDock = UserInterface.Dock.Top;

            titleLabel = new Label("Créer un nouveau monde", new Rectangle(0, 0, 128, 96), Color.White);
            titleLabel.ControlDock = UserInterface.Dock.Top;
            titleLabel.TextStyle = Rendering.SpriteFontDraw.Style.rectangle;
            titleLabel.TextFont = ContentEngine.SpriteFont("Engine", "Bebas_Neue_48pt");

            nameLabel = new Label("Nom du nouveau monde :", new Rectangle(0, 0, 128, 64), Color.White);
            nameLabel.ControlDock = UserInterface.Dock.Top;
            seedLabel = new Label("Graine pour la génération du monde :", new Rectangle(0, 0, 128, 64), Color.White);
            seedLabel.ControlDock = UserInterface.Dock.Top;

            rootContainer.AddChild(controlContainer);
            rootContainer.AddChild(titleLabel);
            rootContainer.AddChild(nameLabel);
            rootContainer.AddChild(worldNameTexBox);
            rootContainer.AddChild(seedLabel);
            rootContainer.AddChild(worldSeedTextBox);

            controlContainer.AddChild(createNewWorldButton);
            controlContainer.AddChild(goBackButton);
        }

        private void CreateNewWorldButton_onMouseClick()
        {
            ThreadStart GenHandle = new ThreadStart(delegate
            {
                Game.sceneManager.RemoveScene(this);
                Game.GameUtils.WorldProperty wrldp = new Game.GameUtils.WorldProperty()
                {
                    WorldName = worldNameTexBox.Text,
                    Seed = int.Parse(worldSeedTextBox.Text)
                };

                Generator.WorldGenerator Gen = new Generator.WorldGenerator(wrldp);
                Game.GameScene wrldsc = Gen.Generate();

                Game.sceneManager.AddScene(wrldsc);

                wrldsc.show();
                Game.sceneManager.RemoveScene(this);
            });

            Thread t = new Thread(GenHandle);
            t.Start();
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
