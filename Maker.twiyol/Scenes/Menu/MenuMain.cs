using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.UserInterface;
using Maker.RiseEngine.Core.UserInterface.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Maker.twiyol.Scenes.Menu
{
    public class MenuMain : Scene
    {
        Texture2D Logo;
        bool asGame = false;
        Game.GameScene CurrentGame;

        // Declaring user inteface elements.
        Panel panelMainMenu;

        Button buttonPlayLastGame;
        Button buttonNewGame;
        Button buttonOpenGame;

        Button buttonOption;
        Button buttonQuitteGame;
        Button buttonQuitte;

        public MenuMain() { asGame = false; }

        public MenuMain(Game.GameScene gameScene)
        {
            CurrentGame = gameScene;
            asGame = true;
        }

        public override void OnLoad()
        {
            Logo = ContentEngine.Texture2D("Engine", "Logo");

            // Panel.
            panelMainMenu = new Panel(new Rectangle(-256, -112, 512, 224), new Color(new Vector4(0f)));
            panelMainMenu.ControlAnchor = Anchor.Center;
            panelMainMenu.Padding = new ControlPadding(16);

            // Create button.
            buttonPlayLastGame = new Button("Reprendre", new Rectangle(0, 0, 480, 64), Color.White);
            buttonNewGame = new Button("Nouvelle Partie", new Rectangle(0, 0, 480, 64), Color.White);
            buttonOpenGame = new Button("Charger une partie", new Rectangle(0, 0, 480, 64), Color.White);

            buttonOption = new Button("Options", new Rectangle(0, 0, 480, 64), Color.White);
            buttonQuitteGame = new Button("Quitter le jeu en cours", new Rectangle(0, 0, 480, 64), Color.White);
            buttonQuitte = new Button("Retour au bureau", new Rectangle(0, 0, 480, 64), Color.White);

            if (!asGame)
            {
                buttonQuitteGame.Visible = false;
                buttonPlayLastGame.Visible = false;
            }

            // Create event handle.
            buttonPlayLastGame.onMouseClick += ButtonPlayLastGame_onMouseClick;
            buttonQuitteGame.onMouseClick += ButtonQuitteGame_onMouseClick;
            buttonNewGame.onMouseClick += ButtonNewGame_onMouseClick;
            buttonQuitte.onMouseClick += ButtonQuitte_onMouseClick;

            // Add child control to root panel.
            panelMainMenu.AddChild(buttonPlayLastGame);
            panelMainMenu.AddChild(buttonNewGame);
            panelMainMenu.AddChild(buttonOpenGame);
            panelMainMenu.AddChild(buttonOption);
            panelMainMenu.AddChild(buttonQuitteGame);
            panelMainMenu.AddChild(buttonQuitte);

            foreach (var i in panelMainMenu.Childs)
            {
                i.ControlDock = Dock.Top;
            }

        }

        private void ButtonQuitteGame_onMouseClick()
        {
            CurrentGame.SaveWorld();

            CurrentGame.GameUIScene.GoBackToGame();
            RiseEngine.sceneManager.RemoveScene(CurrentGame.GameUIScene);
            RiseEngine.sceneManager.RemoveScene(CurrentGame);

            MenuMain m = new MenuMain();
            m.show();
            RiseEngine.sceneManager.AddScene(m);

        }

        private void ButtonPlayLastGame_onMouseClick()
        {
            CurrentGame.GameUIScene.GoBackToGame();
        }

        private void ButtonNewGame_onMouseClick()
        {
            this.hide();

            Scene scene = new MenuNewWorld();
            RiseEngine.sceneManager.AddScene(scene);
            scene.show();

            RiseEngine.sceneManager.RemoveScene(this);
        }

        private void ButtonQuitte_onMouseClick()
        {

        }

        // Scene event handling.
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(Logo, new Vector2(Engine.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2 + 2, Engine.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230 + 2), new Color(0, 0, 0, 125));
            spriteBatch.Draw(Logo, new Vector2(Engine.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2, Engine.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230));

            panelMainMenu.Draw(spriteBatch, gameTime);

        }


        public override void OnUpdate(PlayerInput playerInput, GameTime gameTime)
        {

            panelMainMenu.Update(playerInput, gameTime);

        }

        public override void OnUnload()
        {



        }
    }
}
