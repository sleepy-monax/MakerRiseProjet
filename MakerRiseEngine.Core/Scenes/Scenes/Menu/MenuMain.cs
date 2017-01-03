using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Windows.Forms;

namespace Maker.RiseEngine.Core.Scenes.Scenes.Menu
{
    public class MenuMain : Scene
    {
        Texture2D Logo;
        bool asGame = false;
        Game.GameScene CurrentGame;

        // Declaring user inteface elements.
        UserInterface.Controls.Panel panelMainMenu;

        UserInterface.Controls.Button buttonPlayLastGame;
        UserInterface.Controls.Button buttonNewGame;
        UserInterface.Controls.Button buttonOpenGame;

        UserInterface.Controls.Button buttonOption;
        UserInterface.Controls.Button buttonQuitteGame;
        UserInterface.Controls.Button buttonQuitte;

        public MenuMain() { asGame = false; }

        public MenuMain(Game.GameScene gameScene) {
            CurrentGame = gameScene;
            asGame = true;
        }

        public override void OnLoad()
        {
            Logo = ContentEngine.Texture2D("Engine", "Logo");

            // Panel.
            panelMainMenu = new UserInterface.Controls.Panel(new Rectangle(-256, -112, 512, 224), new Color(new Vector4(0f)));
            panelMainMenu.ControlAnchor = UserInterface.Anchor.Center;
            panelMainMenu.Padding = new UserInterface.ControlPadding(16);

            // Create button.
            buttonPlayLastGame = new UserInterface.Controls.Button("Reprendre", new Rectangle(0, 0, 480, 64), Color.White);
            buttonNewGame = new UserInterface.Controls.Button("Nouvelle Partie", new Rectangle(0, 0, 480, 64), Color.White);
            buttonOpenGame = new UserInterface.Controls.Button("Charger une partie", new Rectangle(0, 0, 480, 64), Color.White);

            buttonOption = new UserInterface.Controls.Button("Options", new Rectangle(0, 0, 480, 64), Color.White);
            buttonQuitteGame = new UserInterface.Controls.Button("Quitter le jeu en cours", new Rectangle(0, 0, 480, 64), Color.White);
            buttonQuitte = new UserInterface.Controls.Button("Retour au bureau", new Rectangle(0, 0, 480, 64), Color.White);

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
                i.ControlDock = UserInterface.Dock.Top;
            }

        }

        private void ButtonQuitteGame_onMouseClick()
        {
            CurrentGame.SaveWorld();

            CurrentGame.GameUIScene.GoBackToGame();
            Game.sceneManager.RemoveScene(CurrentGame.GameUIScene);
            Game.sceneManager.RemoveScene(CurrentGame);

            MenuMain m = new MenuMain();
            m.show();
            Game.sceneManager.AddScene(m);

        }

        private void ButtonPlayLastGame_onMouseClick()
        {
            CurrentGame.GameUIScene.GoBackToGame();
        }

        private void ButtonNewGame_onMouseClick()
        {
            this.hide();

            Scene scene = new MenuNewWorld();
            Game.sceneManager.AddScene(scene);
            scene.show();

            Game.sceneManager.RemoveScene(this);
        }

        private void ButtonQuitte_onMouseClick()
        {
            Application.Exit();
        }

        // Scene event handling.
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(Logo, new Vector2(Engine.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2 + 2, Engine.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230 + 2), new Color(0, 0, 0, 125));
            spriteBatch.Draw(Logo, new Vector2(Engine.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2, Engine.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230));

            panelMainMenu.Draw(spriteBatch, gameTime);

        }


        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {

            panelMainMenu.Update(mouse, keyBoard, gameTime);

        }

        public override void OnUnload()
        {



        }
    }
}
