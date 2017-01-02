using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Maker.RiseEngine.Core.UserInterface.Controls;

namespace Maker.RiseEngine.Core.Game
{
    public class GameUIScene : SceneManager.Scene
    {

        GameScene G;
        SceneManager.Scenes.Menu.MenuMain MainMenu;
        bool IsPause = false;

        public GameUIScene(GameScene _gameScene)
        {
            G = _gameScene;
        }

        public override void OnLoad()
        {

        }

        public override void OnUnload()
        {

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        KeyboardState oldKeyBoard;
        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            if (keyBoard.IsKeyDown(Engine.engineConfig.Input_ShowMenu) && oldKeyBoard.IsKeyUp(Engine.engineConfig.Input_ShowMenu))
            {
                if (!IsPause)
                    PauseGame();

            }

            oldKeyBoard = keyBoard;
        }

        public void PauseGame()
        {
            IsPause = true;
            G.PauseSimulation = true;
            MainMenu = new SceneManager.Scenes.Menu.MenuMain(G);
            Game.sceneManager.AddScene(MainMenu);
            MainMenu.show();
        }

        public void GoBackToGame() {

            Game.sceneManager.RemoveScene(MainMenu);
            G.PauseSimulation = false;
            IsPause = false;

        }
    }
}
