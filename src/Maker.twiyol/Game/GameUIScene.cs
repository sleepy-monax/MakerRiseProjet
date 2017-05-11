using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Scenes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.Twiyol.Game
{
    public class GameUIScene : Scene
    {

        GameScene G;
        Scenes.Menu.MenuMain MainMenu;
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
            if (playerInput.IsKeyBoardKeyPress(rise.engineConfig.Input_ShowMenu))
            {
                if (!IsPause)
                    PauseGame();

            }

            if (playerInput.IsKeyBoardKeyPress(rise.engineConfig.Input_ShowInventory)) {
                if (!IsPause)
                    ShowInventory();

            }

        }

        public void ShowInventory() {
            IsPause = true;
            G.PauseSimulation = true;

            GameUI.Inventory inventoryScene = new GameUI.Inventory(G.World.playerEntity, G);
            RiseEngine.ScenesManager.AddScene(inventoryScene);
            inventoryScene.show();
        }

        public void PauseGame()
        {
            IsPause = true;
            G.PauseSimulation = true;
            MainMenu = new Scenes.Menu.MenuMain(G);
            RiseEngine.ScenesManager.AddScene(MainMenu);
            MainMenu.show();
        }

        public void GoBackToGame(Scene currentMenu)
        {

            RiseEngine.ScenesManager.RemoveScene(currentMenu);
            G.PauseSimulation = false;
            IsPause = false;

        }
    }
}
