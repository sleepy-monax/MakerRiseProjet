
using Maker.RiseEngine.Input;
using Maker.RiseEngine.Rendering;
using Maker.RiseEngine.Scenes;
using Maker.RiseEngine.UserInterface;
using Maker.RiseEngine.UserInterface.Controls;
using Maker.Twiyol.Game.GameUtils;
using Maker.Twiyol.Generator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace Maker.Twiyol.Scenes.Menu
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
            rootContainer.Anchor = Anchors.Center;

            titleLabel = new Label("Nouveau monde", new Rectangle(0, 0, 64, 64), Color.White);
            titleLabel.Dock = Docks.Top;
            titleLabel.TextStyle = Style.rectangle;
            titleLabel.TextFont = Engine.ressourceManager.GetSpriteFont("Engine", "Bebas_Neue_48pt");

            controlContainer = new Panel(new Rectangle(0, 0, 0, 96), Color.White);
            controlContainer.Padding = new ControlPadding(16);
            controlContainer.Dock = Docks.Bottom;

            createNewWorldButton = new Button("Créer", new Rectangle(0, 0, 200, 64), Color.White);
            createNewWorldButton.Dock = Docks.Right;
            createNewWorldButton.MouseClick += CreateNewWorldButton_onMouseClick;

            goBackButton = new Button("Retour", new Rectangle(0, 0, 200, 64), Color.White);
            goBackButton.Dock = Docks.Left;
            goBackButton.MouseClick += GoBackButton_onMouseClick;

            worldNameTexBox = new TextBox("Monde sans nom", new Rectangle(0, 0, 128, 64), Color.White, Color.Black);
            worldNameTexBox.Dock = Docks.Top;

            worldSeedTextBox = new TextBox(new Random().Next().ToString(), new Rectangle(0, 0, 128, 64), Color.White, Color.Black);
            worldSeedTextBox.Dock = Docks.Top;

            nameLabel = new Label("Nom du nouveau monde :", new Rectangle(0, 0, 128, 64), Color.White);
            nameLabel.Dock = Docks.Top;
            nameLabel.TextAlignment = Alignment.Left;


            seedLabel = new Label("Graine de la génération :", new Rectangle(0, 0, 128, 64), Color.White);
            seedLabel.Dock = Docks.Top;
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

        private void CreateNewWorldButton_onMouseClick(Control sender) { 
       
            ThreadStart GenHandle = new ThreadStart(delegate
            {
                Engine.sceneManager.RemoveScene(this);

                int result;
                int.TryParse(worldSeedTextBox.Text, out result);

                GeneratorProperty generatorProperty = new GeneratorProperty(worldNameTexBox.Text, result);

                WorldGenerator Gen = new WorldGenerator(generatorProperty);
                var world = Gen.Generate();
                Game.GameScene wrldsc = new Game.GameScene(world);

                Engine.sceneManager.AddScene(wrldsc);

                wrldsc.Show();
            });

            Thread t = new Thread(GenHandle);
            t.Start();
        }

        private void GoBackButton_onMouseClick(Control sender)
        {
            Scene menu = new MainMenu();
            Engine.sceneManager.AddScene(menu);
            menu.Show();
            Engine.sceneManager.RemoveScene(this);
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
