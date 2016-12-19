using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.SceneManager.Scenes.Menu
{
    public class MenuMain : Scene
    {
        // Declaring usre inteface ellements.
        UserInterface.Controls.Panel panelMainMenu;

        UserInterface.Controls.Button buttonPlay;
        UserInterface.Controls.Button buttonOption;
        UserInterface.Controls.Button buttonQuitte;


        public override void OnLoad()
        {

            panelMainMenu = new UserInterface.Controls.Panel(new Rectangle(0, 0, 512, 276), Color.White);
            panelMainMenu.ControlAnchor = UserInterface.Anchor.Center;

            buttonPlay = new UserInterface.Controls.Button("Jouer", new Rectangle(16, 16, 480, 64), Color.White);
            buttonOption = new UserInterface.Controls.Button("Options", new Rectangle(16, 96, 480, 64), Color.White);
            buttonQuitte = new UserInterface.Controls.Button("Quitter le jeu", new Rectangle(16, 192, 480, 64), Color.White);

            panelMainMenu.AddChild(buttonPlay);
            panelMainMenu.AddChild(buttonOption);
            panelMainMenu.AddChild(buttonQuitte);

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

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
