using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Ressources;
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
        Panel rootPanel;

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
            Logo = rise.ENGINE.RESSOUCES.Texture2D("Engine", "Logo");

            // Panel.
            rootPanel = new Panel(new Rectangle(-256, -112, 512, 224), new Color(new Vector4(0f)));
            rootPanel.ControlAnchor = Anchor.Center;
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
            buttonPlayLastGame.onMouseClick += ButtonPlayLastGame_onMouseClick;
            buttonQuitteGame.onMouseClick += ButtonQuitteGame_onMouseClick;
            buttonNewGame.onMouseClick += ButtonNewGame_onMouseClick;
            buttonQuitte.onMouseClick += ButtonQuitte_onMouseClick;
            buttonOpenGame.onMouseClick += ButtonOpenGame_onMouseClick;
            buttonOption.onMouseClick += ButtonOption_onMouseClick;

            // Add child control to root panel.
            rootPanel.AddChild(buttonPlayLastGame);
            rootPanel.AddChild(buttonNewGame);
            rootPanel.AddChild(buttonOpenGame);
            rootPanel.AddChild(buttonOption);
            rootPanel.AddChild(buttonQuitteGame);
            rootPanel.AddChild(buttonQuitte);

            foreach (var i in rootPanel.Childs)
            {
                i.ControlDock = Dock.Top;
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

            ENGINE.SCENES.AddScene(scene);
            scene.show();

            ENGINE.SCENES.RemoveScene(this);
        }

        private void ButtonOpenGame_onMouseClick()
        {

            this.hide();

            Scene scene = new MenuOpenWorld();
            ENGINE.SCENES.AddScene(scene);
            scene.show();

            ENGINE.SCENES.RemoveScene(this);

        }

        private void ButtonQuitteGame_onMouseClick()
        {
            CurrentGame.SaveWorld();

            CurrentGame.GameUIScene.GoBackToGame(this);
            ENGINE.SCENES.RemoveScene(CurrentGame.GameUIScene);
            ENGINE.SCENES.RemoveScene(CurrentGame);

            MenuMain m = new MenuMain();
            m.show();
            ENGINE.SCENES.AddScene(m);

        }

        private void ButtonPlayLastGame_onMouseClick()
        {
            CurrentGame.GameUIScene.GoBackToGame(this);
        }

        private void ButtonNewGame_onMouseClick()
        {
            this.hide();

            Scene scene = new MenuNewWorld();
            ENGINE.SCENES.AddScene(scene);
            scene.show();

            ENGINE.SCENES.RemoveScene(this);
        }

        private void ButtonQuitte_onMouseClick()
        {
            CurrentGame?.SaveWorld();
            rise.STOP();
        }


        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            spriteBatch.Draw(Logo, new Vector2(rise.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2 + 2, rise.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230 + 2), new Color(0, 0, 0, 125));
            spriteBatch.Draw(Logo, new Vector2(rise.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2, rise.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230));

            rootPanel.Draw(spriteBatch, gameTime);

        }


        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {

            if (playerInput.IsKeyBoardKeyPress(rise.engineConfig.Input_ShowMenu)) {
                CurrentGame.GameUIScene.GoBackToGame(this);
            }
            

            rootPanel.Update(playerInput, gameTime);

        }

        public override void OnUnload()
        {



        }
    }
}
