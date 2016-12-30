using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Windows.Forms;

namespace Maker.RiseEngine.Core.SceneManager.Scenes.Menu
{
    public class MenuMain : Scene
    {

        // BackGround.
        SpriteBatch BackgroundSB;
        Parallax Background;

        Texture2D Logo;

        // Declaring user inteface elements.
        UserInterface.Controls.Panel panelMainMenu;

        UserInterface.Controls.Button buttonPlay;
        UserInterface.Controls.Button buttonOption;
        UserInterface.Controls.Button buttonQuitte;

        public override void OnLoad()
        {
            //Back Ground.
            BackgroundSB = new SpriteBatch(Engine.GraphicsDevice);
            switch (new Random().Next(3))
            {
                case 0:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Dusk Mountain", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));
                    Audio.SongEngine.SwitchSong("Engine", "A Title");
                    break;
                case 1:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Forest", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));
                    Audio.SongEngine.SwitchSong("Engine", "Look Up");
                    break;
                case 2:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight));
                    Audio.SongEngine.SwitchSong("Engine", "Clouds of Orange Juice");
                    break;
                default:
                    break;
            }

            Logo = ContentEngine.Texture2D("Engine", "Logo");

            // Panel.
            panelMainMenu = new UserInterface.Controls.Panel(new Rectangle(-256, -112, 512, 224), new Color(new Vector4(0f)));
            panelMainMenu.ControlAnchor = UserInterface.Anchor.Center;
            panelMainMenu.Padding = new UserInterface.ControlPadding(16);

            // Create button.
            buttonPlay = new UserInterface.Controls.Button("Jouer", new Rectangle(0, 0, 480, 64), Color.White);
            buttonOption = new UserInterface.Controls.Button("Options", new Rectangle(0, 0, 480, 64), Color.White);
            buttonQuitte = new UserInterface.Controls.Button("Quitter le jeu", new Rectangle(0, 0, 480, 64), Color.White);

            // Create event handle.
            buttonPlay.onMouseClick += ButtonPlay_onMouseClick;
            buttonOption.onMouseClick += ButtonOption_onMouseClick;
            buttonQuitte.onMouseClick += ButtonQuitte_onMouseClick;

            // Add child control to root panel.
            panelMainMenu.AddChild(buttonPlay);
            panelMainMenu.AddChild(buttonOption);
            panelMainMenu.AddChild(buttonQuitte);

            foreach (var i in panelMainMenu.Childs)
            {
                i.ControlDock = UserInterface.Dock.Up;
            }

        }

        // Event handeling.
        private void ButtonPlay_onMouseClick()
        {
            this.hide();

            Scene scene = new MenuOpenWorld();
            Game.sceneManager.AddScene(scene);
            scene.show();

            Game.sceneManager.RemoveScene(this);
        }

        private void ButtonOption_onMouseClick()
        {

        }

        private void ButtonQuitte_onMouseClick()
        {
            Application.Exit();
        }

        // Scene event handling.
        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, gameTime);
            BackgroundSB.End();

            spriteBatch.FillRectangle(new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 128));

            spriteBatch.Draw(Logo, new Vector2(Engine.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2 + 2, Engine.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230 + 2), new Color(0, 0, 0, 125));
            spriteBatch.Draw(Logo, new Vector2(Engine.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2, Engine.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230));


            panelMainMenu.Draw(spriteBatch, gameTime);

        }


        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {

            Background.Update(mouse, keyBoard, gameTime);
            panelMainMenu.Update(mouse, keyBoard, gameTime);

        }

        public override void OnUnload()
        {



        }
    }
}
