using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiseEngine.Core.World.Utils
{
    public class GameUI : Idrawable
    {
        WorldScene W;

        public UI.ContainerManager cManager;
        UI.ContainerManager OptionMenuManager;

        //pauseMenu
        UI.Container PauseMenu;

        UI.Controls.Button PauseButResume;
        UI.Controls.Button PauseButSave;
        UI.Controls.Button PauseButOption;
        UI.Controls.Button PauseButQuit;

        //GameUI

        UI.Container GameUIContainer;

        UI.Controls.MiniMap miniMap;

        //Chat

        UI.Container ChatContainer;
        UI.Controls.TextBox ChatInput;



        public GameUI(WorldScene _WorldScene)
        {
            W = _WorldScene;

            cManager = new UI.ContainerManager();

            //PauseMenu
            PauseMenu = new UI.Container(new Rectangle(-256, -206, 512, 384), true, UI.Dock.Center, Color.White);
            PauseMenu.Title = "Pause";
            PauseMenu.ShowTitle = true;

            PauseButResume = new UI.Controls.Button("Reprendre", 480, 16, 96, Color.White);
            PauseButResume.OnMouseClick += ResumeGame;
            PauseButSave = new UI.Controls.Button("Sauvegarder", 480, 16, 160, Color.White);
            PauseButOption = new UI.Controls.Button("Option", 480, 16, 224, Color.White);
            PauseButOption.OnMouseClick += ShowOption;
            PauseButQuit = new UI.Controls.Button("Quitter", 480, 16, 304, Color.White);
            PauseButQuit.OnMouseClick += PauseButQuit_OnMouseClick;

            PauseMenu.Controls.Add(PauseButResume);
            PauseMenu.Controls.Add(PauseButSave);
            PauseMenu.Controls.Add(PauseButOption);
            PauseMenu.Controls.Add(PauseButQuit);

            //Option
            cManager.AddContainer("Option", Scene.SceneManager.MainMn.OptionContainer);
            OptionMenuManager = Scene.SceneManager.MainMn.OptionMenuManager;
            Scene.SceneManager.MainMn.OptionBackButton.OnMouseClick += OptionBack;

            cManager.AddContainer("PauseMenu", PauseMenu);

            //GameUI

            GameUIContainer = new UI.Container(new Rectangle(0, 0, Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight), false, UI.Dock.UpLeft, Color.White);
            miniMap = new UI.Controls.MiniMap(16, 16, W);

            GameUIContainer.Controls.Add(miniMap);
            cManager.AddContainer("GameUI", GameUIContainer);

            //Chat

            ChatContainer = new UI.Container(new Rectangle(0, -64, 64, 620), false, UI.Dock.DownLeft, Color.White);
            ChatInput = new UI.Controls.TextBox("", 32, 0, 0);

            ChatContainer.Controls.Add(ChatInput);
            cManager.AddContainer("Chat", ChatContainer);

            cManager.SwitchContainer("GameUI");
        }


        KeyboardState PasteKeyboard = Keyboard.GetState();
        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            if (KeyBoard.IsKeyUp(Config.Controls.ShowMenu) && PasteKeyboard.IsKeyDown(Config.Controls.ShowMenu))
            {

                W.TogglePauseGame();

            }


            if (KeyBoard.IsKeyUp(Config.Controls.ShowChat) && PasteKeyboard.IsKeyDown(Config.Controls.ShowChat))
            {

                if (cManager.CurrentContainerKey == "GameUI") {

                    W.Pause = true;
                    cManager.SwitchContainer("Chat");

                }

            }

            PasteKeyboard = KeyBoard;

            cManager.Update(Mouse, KeyBoard, gameTime);



            if (cManager.CurrentContainerKey == "Option")
            {
                OptionMenuManager.Update(Mouse, KeyBoard, gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            cManager.Draw(spriteBatch, gameTime);

            if (cManager.CurrentContainerKey == "Option")
            {
                OptionMenuManager.Draw(spriteBatch, gameTime);
            }

        }

        #region PauseEvent

        

        private void ResumeGame()
        {
            cManager.SwitchContainer("GameUI");
            W.Pause = false;
        }
        private void ShowOption()
        {

            cManager.SwitchContainer("Option");

        }

        private void OptionBack()
        {
            cManager.SwitchContainer("PauseMenu");
        }

        private void PauseButQuit_OnMouseClick()
        {
            W.StopGame();
        }
        #endregion

    }
}
