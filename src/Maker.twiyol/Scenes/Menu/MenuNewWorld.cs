using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.UserInterface;
using Maker.RiseEngine.Core.UserInterface.Controls;
using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.Generator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace Maker.twiyol.Scenes.Menu
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
            rootContainer.Padding = new ControlPadding(16);
            rootContainer.ControlAnchor = Anchor.Center;

            titleLabel = new Label("Nouveau monde", new Rectangle(0, 0, 64, 64), Color.White);
            titleLabel.ControlDock = Dock.Top;
            titleLabel.TextStyle = Style.rectangle;
            titleLabel.TextFont = ENGINE.RESSOUCES.SpriteFont("Engine", "Bebas_Neue_48pt");

            controlContainer = new Panel(new Rectangle(0, 0, 0, 96), Color.White);
            controlContainer.Padding = new ControlPadding(16);
            controlContainer.ControlDock = Dock.Bottom;

            createNewWorldButton = new Button("Créer", new Rectangle(0, 0, 200, 64), Color.White);
            createNewWorldButton.ControlDock = Dock.Right;
            createNewWorldButton.onMouseClick += CreateNewWorldButton_onMouseClick;

            goBackButton = new Button("Retour", new Rectangle(0, 0, 200, 64), Color.White);
            goBackButton.ControlDock = Dock.Left;
            goBackButton.onMouseClick += GoBackButton_onMouseClick;

            worldNameTexBox = new TextBox("Monde sans nom", new Rectangle(0, 0, 128, 64), Color.White, Color.Black);
            worldNameTexBox.ControlDock = Dock.Top;

            worldSeedTextBox = new TextBox(new Random().Next().ToString(), new Rectangle(0, 0, 128, 64), Color.White, Color.Black);
            worldSeedTextBox.ControlDock = Dock.Top;

            nameLabel = new Label("Nom du nouveau monde :", new Rectangle(0, 0, 128, 64), Color.White);
            nameLabel.ControlDock = Dock.Top;
            nameLabel.TextAlignment = Alignment.Left;


            seedLabel = new Label("Graine de la génération :", new Rectangle(0, 0, 128, 64), Color.White);
            seedLabel.ControlDock = Dock.Top;
            seedLabel.TextAlignment = Alignment.Left;

            rootContainer.AddChild(controlContainer);
            rootContainer.AddChild(titleLabel);
            rootContainer.AddChild(nameLabel);
            rootContainer.AddChild(worldNameTexBox);
            rootContainer.AddChild(seedLabel);
            rootContainer.AddChild(worldSeedTextBox);

            controlContainer.AddChild(createNewWorldButton);
            controlContainer.AddChild(goBackButton);
        }

        private void CreateNewWorldButton_onMouseClick() { 
       
            ThreadStart GenHandle = new ThreadStart(delegate
            {
                ENGINE.SCENES.RemoveScene(this);

                int result;
                int.TryParse(worldSeedTextBox.Text, out result);

                GeneratorProperty generatorProperty = new GeneratorProperty(worldNameTexBox.Text, result);

                WorldGenerator Gen = new WorldGenerator(generatorProperty);
                var world = Gen.Generate();
                Game.GameScene wrldsc = new Game.GameScene(world);

                ENGINE.SCENES.AddScene(wrldsc);

                wrldsc.show();
                ENGINE.SCENES.RemoveScene(this);
            });

            Thread t = new Thread(GenHandle);
            t.Start();
        }

        private void GoBackButton_onMouseClick()
        {
            Scene menu = new MenuMain();
            ENGINE.SCENES.AddScene(menu);
            menu.show();
            ENGINE.SCENES.RemoveScene(this);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            rootContainer.Draw(spriteBatch, gameTime);

        }

        public override void OnUnload()
        {



        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {

            rootContainer.Update(playerInput, gameTime);

        }

    }
}
