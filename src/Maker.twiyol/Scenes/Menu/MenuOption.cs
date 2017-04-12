using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.UserInterface;
using Maker.RiseEngine.Core.UserInterface.Controls;

using Maker.twiyol.Game;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace Maker.twiyol.Scenes.Menu
{

    public class MenuOption : Scene
    {

        Panel rootPanel;

        Label titleLabel;

        Button buttonInput;
        Button buttonGameplay;
        Button buttonSound;
        Button buttonGraphics;
        Button buttonBack;

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

            rootPanel = new Panel(new Rectangle(-256, -208, 512, 416), Color.Transparent);
            rootPanel.ControlAnchor = Anchor.Center;
            rootPanel.Padding = new ControlPadding(16);
            rootPanel.ChildMargin = new ControlPadding(16);

            titleLabel = new Label("Options", new Rectangle(0, -80, 512, 64), Color.White);
            titleLabel.TextStyle = Style.rectangle;
            titleLabel.TextFont = ENGINE.RESSOUCES.SpriteFont("Engine", "Bebas_Neue_48pt");

            buttonInput = new Button("Controls", new Rectangle(0, 0, 64, 64), Color.White);
            buttonInput.ControlDock = Dock.Top;

            buttonGameplay = new Button("Jeux", new Rectangle(0, 0, 64, 64), Color.White);
            buttonGameplay.ControlDock = Dock.Top;

            buttonSound = new Button("Sons", new Rectangle(0, 0, 64, 64), Color.White);
            buttonSound.ControlDock = Dock.Top;

            buttonGraphics = new Button("Graphismes", new Rectangle(0, 0, 64, 64), Color.White);
            buttonGraphics.ControlDock = Dock.Top;

            buttonBack = new Button("Retour", new Rectangle(0, 0, 64, 64), Color.White);
            buttonBack.ControlDock = Dock.Top;
            buttonBack.onMouseClick += GoBack;

            rootPanel.AddChild(titleLabel);
            rootPanel.AddChild(buttonInput);
            rootPanel.AddChild(buttonGameplay);
            rootPanel.AddChild(buttonSound);
            rootPanel.AddChild(buttonGraphics);
            rootPanel.AddChild(buttonBack);

        }

        private void GoBack()
        {

            Scene menu;

            if (asGame) {

                menu = new MenuMain(CurrentGame);

            } else {

                menu = new MenuMain();

            }

            ENGINE.SCENES.AddScene(menu);
            menu.show();
            ENGINE.SCENES.RemoveScene(this);
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