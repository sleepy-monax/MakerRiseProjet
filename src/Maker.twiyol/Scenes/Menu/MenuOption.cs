using Maker.RiseEngine.Core;

using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.UserInterface;
using Maker.RiseEngine.Core.UserInterface.Controls;

using Maker.Twiyol.Game;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace Maker.Twiyol.Scenes.Menu
{

    public class MenuOption : Scene
    {
        // The root panel of the GUI.
        Panel rootPanel;

        // Title of the GUI.
        Label titleLabel;

        // Button to switch to other option.
        Button buttonInput;
        Button buttonGameplay;
        Button buttonSound;
        Button buttonGraphics;
        Button buttonBack;

        // Get if a game is curently running.
        bool asGame;
        GameScene CurrentGame;

        public MenuOption() {
            asGame = false;
        }

        public MenuOption(GameScene currentGame)
        {
            asGame = true;
            CurrentGame = currentGame;
        }

        public override void OnLoad()
        {

            rootPanel = new Panel(new Rectangle(-256, -208, 512, 416), Color.Transparent)
            {
                Anchor = Anchors.Center,
                Padding = new ControlPadding(16),
                ChildMargin = new ControlPadding(16)
            };

            titleLabel = new Label("Options", new Rectangle(0, -80, 512, 64), Color.White);
            titleLabel.TextStyle = Style.rectangle;
            titleLabel.TextFont = Engine.ressourceManager.GetSpriteFont("Engine", "Bebas_Neue_48pt");

            buttonInput = new Button("Controls", new Rectangle(0, 0, 64, 64), Color.White);
            buttonInput.Dock = Docks.Top;

            buttonGameplay = new Button("Jeux", new Rectangle(0, 0, 64, 64), Color.White);
            buttonGameplay.Dock = Docks.Top;

            buttonSound = new Button("Sons", new Rectangle(0, 0, 64, 64), Color.White);
            buttonSound.Dock = Docks.Top;

            buttonGraphics = new Button("Graphismes", new Rectangle(0, 0, 64, 64), Color.White);
            buttonGraphics.Dock = Docks.Top;

            buttonBack = new Button("Retour", new Rectangle(0, 0, 64, 64), Color.White);
            buttonBack.Dock = Docks.Top;
            buttonBack.MouseClick += GoBack;

            rootPanel.AddChild(titleLabel);
            rootPanel.AddChild(buttonInput);
            rootPanel.AddChild(buttonGameplay);
            rootPanel.AddChild(buttonSound);
            rootPanel.AddChild(buttonGraphics);
            rootPanel.AddChild(buttonBack);

        }

        private void GoBack()
        {
            // Go back to the MainMenu.
            Scene menu;

            if (asGame) menu = new MainMenu(CurrentGame);
            else menu = new MainMenu();
            // Hide the current scene and show the main menu.            
            Engine.sceneManager.AddScene(menu);
            menu.Show();
            Engine.sceneManager.RemoveScene(this);
        }

        public override void OnUnload()
        {

            

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            rootPanel.Draw(spriteBatch, gameTime);
        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            rootPanel.Update(playerInput, gameTime);
        }

    }
}