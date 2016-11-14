using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiseEngine.Core.GameScene
{
    public static class GameSceneManager
    {

        static GameScene currentGameScene;


        public static void SwitchGameScene(GameScene newGameScene) {

            currentGameScene?.UnloadScene();
            currentGameScene = newGameScene;
            currentGameScene.Initialize();

        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentGameScene.Draw(spriteBatch, gameTime);

        }

        public static void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            currentGameScene.Update(Mouse, KeyBoard, gameTime);
        }
    }
}
