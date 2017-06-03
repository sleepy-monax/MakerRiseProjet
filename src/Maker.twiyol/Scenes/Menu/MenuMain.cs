using Maker.RiseEngine.Core;

using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.UserInterface;
using Maker.RiseEngine.Core.UserInterface.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Maker.Twiyol.Scenes.Menu
{
    public class MainMenu : Scene
    {
        Texture2D Logo;
        bool asGame = false;
        Game.GameScene CurrentGame;
        
        // Declaring user inteface elements.
        Panel rootPanel;

        Button buttonPlayLastGame;
        Button buttonNewGame;
        Button buttonOpenGame;

        Button buttonOption;
        Button buttonQuitteGame;
        Button buttonQuitte;

        public MainMenu() { asGame = false; }

        public MainMenu(Game.GameScene gameScene)
        {
            CurrentGame = gameScene;
            asGame = true;
        }

        public override void OnLoad()
        {
            Logo = Rise.Engine.ressourceManager.GetTexture2D("Engine", "Logo");

            // Panel.
            rootPanel = new Panel(new Rectangle(-256, -112, 512, 224), new Color(new Vector4(0f)));
            rootPanel.Anchor = Anchors.Center;
            rootPanel.Padding = new ControlPadding(16);
            rootPanel.ChildMargin = new ControlPadding(16);

            // Create button.
            buttonPlayLastGame = new Button("Reprendre", new Rectangle(0, 0, 480, 64), Color.White);
            buttonNewGame = new Button("Nouveau monde", new Rectangle(0, 0, 480, 64), Color.White);
            buttonOpenGame = new Button("Charger un monde", new Rectangle(0, 0, 480, 64), Color.White);

            buttonOption = new Button("Options", new Rectangle(0, 0, 480, 64), Color.White);
            buttonQuitteGame = new Button("Quitter le jeu en cours", new Rectangle(0, 0, 480, 64), Color.White);
            buttonQuitte = new Button("Retour au bureau", new Rectangle(0, 0, 480, 64), Color.White);

            if (!asGame)
            {
                buttonQuitteGame.Visible = false;
                buttonPlayLastGame.Visible = false;
            }
            else {

                buttonNewGame.Visible = false;
                buttonOpenGame.Visible = false;
            }


            // Create event handle.
            buttonPlayLastGame.MouseClick += ButtonPlayLastGame_onMouseClick;
            buttonQuitteGame.MouseClick += ButtonQuitteGame_onMouseClick;
            buttonNewGame.MouseClick += ButtonNewGame_onMouseClick;
            buttonQuitte.MouseClick += ButtonQuitte_onMouseClick;
            buttonOpenGame.MouseClick += ButtonOpenGame_onMouseClick;
            buttonOption.MouseClick += ButtonOption_onMouseClick;

            // Add child control to root panel.
            rootPanel.AddChild(buttonPlayLastGame);
            rootPanel.AddChild(buttonNewGame);
            rootPanel.AddChild(buttonOpenGame);
            rootPanel.AddChild(buttonOption);
            rootPanel.AddChild(buttonQuitteGame);
            rootPanel.AddChild(buttonQuitte);

            foreach (var i in rootPanel.ChildsControls)
            {
                i.Dock = Docks.Top;
            }

        }

        private void ButtonOption_onMouseClick()
        {
            this.hide();

            Scene scene;

            if (asGame)
            {
                scene = new MenuOption(CurrentGame);
            }
            else
            {
                scene = new MenuOption();
            }

            Engine.sceneManager.AddScene(scene);
            scene.Show();

            Engine.sceneManager.RemoveScene(this);
        }

        private void ButtonOpenGame_onMouseClick()
        {

            this.hide();

            Scene scene = new MenuOpenWorld();
            Engine.sceneManager.AddScene(scene);
            scene.Show();

            Engine.sceneManager.RemoveScene(this);

        }

        private void ButtonQuitteGame_onMouseClick()
        {
            CurrentGame.SaveWorld();

            CurrentGame.GameUIScene.GoBackToGame(this);
            Engine.sceneManager.RemoveScene(CurrentGame.GameUIScene);
            Engine.sceneManager.RemoveScene(CurrentGame);

            MainMenu m = new MainMenu();
            m.Show();
            Engine.sceneManager.AddScene(m);

        }

        private void ButtonPlayLastGame_onMouseClick()
        {
            CurrentGame.GameUIScene.GoBackToGame(this);
        }

        private void ButtonNewGame_onMouseClick()
        {
            this.hide();

            Scene scene = new MenuNewWorld();
            Engine.sceneManager.AddScene(scene);
            scene.Show();

            Engine.sceneManager.RemoveScene(this);
        }

        private void ButtonQuitte_onMouseClick()
        {
            CurrentGame?.SaveWorld();
            Rise.STOP();
        }


        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(Logo, new Vector2(Engine.graphicsDeviceManager.PreferredBackBufferWidth / 2 - Logo.Width / 2 + 2, Engine.graphicsDeviceManager.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230 + 2), new Color(0, 0, 0, 125));
            spriteBatch.Draw(Logo, new Vector2(Engine.graphicsDeviceManager.PreferredBackBufferWidth / 2 - Logo.Width / 2, Engine.graphicsDeviceManager.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230));

            rootPanel.Draw(spriteBatch, gameTime);

        }


        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {

            if (playerInput.IsKeyBoardKeyPress(Engine.userConfig.InputShowMainMenu)) {
                CurrentGame.GameUIScene.GoBackToGame(this);
            }
            

            rootPanel.Update(playerInput, gameTime);

        }

        public override void OnUnload()
        {



        }
    }
}
