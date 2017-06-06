using Maker.RiseEngine;
using Maker.RiseEngine.Input;
using Maker.RiseEngine.Scenes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.Twiyol.Game
{
    public class GameUIScene : Scene
    {

        GameScene G;
        Scenes.Menu.MainMenu MainMenu;
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

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            if (playerInput.IsKeyBoardKeyPress(Engine.userConfig.InputShowMainMenu))
            {
                if (!IsPause)
                    PauseGame();

            }

            if (playerInput.IsKeyBoardKeyPress(Engine.userConfig.InputShowInventory)) {
                if (!IsPause)
                    ShowInventory();

            }

        }

        public void ShowInventory() {
            IsPause = true;
            G.PauseSimulation = true;

            GameUI.Inventory inventoryScene = new GameUI.Inventory(G.World.playerEntity, G);
            Engine.sceneManager.AddScene(inventoryScene);
            inventoryScene.Show();
        }

        public void PauseGame()
        {
            IsPause = true;
            G.PauseSimulation = true;
            MainMenu = new Scenes.Menu.MainMenu(G);
            Engine.sceneManager.AddScene(MainMenu);
            MainMenu.Show();
        }

        public void GoBackToGame(Scene currentMenu)
        {

            Engine.sceneManager.RemoveScene(currentMenu);
            G.PauseSimulation = false;
            IsPause = false;

        }
    }
}
