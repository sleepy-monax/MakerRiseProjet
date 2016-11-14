using RiseEngine.Core.Rendering.SpriteSheets;
using RiseEngine.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiseEngine.Core.Scene
{
    public struct SceneManager
    {

        public static int CurrentScene = -1;
        public static MainMenu MainMn;
        public static WorldScene Gm;
        static SplashScreen Splash;
        static UItest uiTest;

        static Point MouseXY = Point.Zero;
        static WorldGenerating WG = new WorldGenerating();

        public static void Initialize()
        {
            Splash = new SplashScreen();
            MainMn = new MainMenu();
            uiTest = new UItest();
        }

        //0 = MainMenu
        //1 = Option
        //2 = WorldManager
        //3 = loading

        public static void StartGame(WorldScene game)
        {

            CurrentScene = -1;
            Gm = game;
            Audio.SongEngine.SwitchSong("Engine", "A Title");
            CurrentScene = 1;

        }

        
        public static void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            MouseXY = Mouse.Position;
           
            switch (CurrentScene)
            {
                case -1:
                    //do nothing
                    break;
                case 0:
                    //Update Main Menu
                    MainMn.Update(Mouse, KeyBoard, gameTime);

                    break;

                case 1:

                    //Update Game
                    Gm.Update(Mouse, KeyBoard, gameTime);

                    break;

                case 2:

                    Splash.Update(Mouse, KeyBoard, gameTime);

                    break;
                case 3:
                    uiTest.Update(Mouse, KeyBoard, gameTime);


                    break;
                case 4:



                    break;
            }
            

        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            

            switch (CurrentScene)
            {
                case -1:
                    //do nothing
                    break;
                case 0:
                    //Draw Main menu 
                    MainMn.Draw(spriteBatch, gameTime);

                    break;
                case 1:
                    // Draw Game
                    Gm.Draw(spriteBatch, gameTime);

                    break;

                case 2:

                    Splash.Draw(spriteBatch, gameTime);

                    break;
                case 3:

                    uiTest.Draw(spriteBatch, gameTime);

                    break;
                case 4:
                    WG.Draw(spriteBatch, gameTime);
                    break;
            }
            

        }

    }
}
