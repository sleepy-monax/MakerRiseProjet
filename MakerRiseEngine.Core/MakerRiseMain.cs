using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core
{
    public class RiseGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Core.EngineDebug.DebugScreen DbgScr;
        bool GLmode = false;

        public SceneManager.SceneManager sceneManager;

        public RiseGame(bool _Glmode)
        {
            graphics = new GraphicsDeviceManager(this);
            Engine.graphics = graphics;
            Content.RootDirectory = "Data";
            Engine.MainGame = this;
            Engine.Window = Window;
            Engine.GameForm = (Form)Control.FromHandle(Window.Handle);

            sceneManager = new SceneManager.SceneManager(this);

            GLmode = _Glmode;
        }

        protected override void Initialize()
        {
            EngineDebug.DebugLogs.WriteInLogs("Initializing game engine...", EngineDebug.LogType.Info, "Core");

            //setting up screen
            if (Engine.engineConfig.GFX_FullScreen == true)
            {
                // Set full screen.
                graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
                graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            }
            else
            {
                // Set window setting.
                graphics.PreferredBackBufferWidth = 1366;
                graphics.PreferredBackBufferHeight = 768;
                graphics.ApplyChanges();
            }

            // Set windows from property.
            Window.Title = "Rise : Le monde est votre seule limite";
            Window.AllowAltF4 = false;
            Window.AllowUserResizing = false;
            Window.IsBorderless = false;

            // Setup debug console.
            System.Console.Title = "Maker Rise Engine Debug Tool - " + Engine.Version.ToString();

            // Hide the systeme mouse cursor.
            this.IsMouseVisible = false;

            base.Initialize();
        }



        protected override void LoadContent()
        {
            EngineDebug.DebugLogs.WriteInLogs("LoadContent...", Core.EngineDebug.LogType.Info, "Core");

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Engine.GraphicsDevice = this.GraphicsDevice;
            ContentEngine.Content = this.Content;
            Rendering.SpriteSheets.CommonSheets.Load();

            DbgScr = new Core.EngineDebug.DebugScreen();
            //Scene.SceneManager.Initialize();
            //Scene.SceneManager.CurrentScene = 2;

            // Show the loading scene.
            sceneManager.AddScene(new SceneManager.Scenes.Menu.MenuBackground());
            sceneManager.AddScene(new SceneManager.Scenes.EngineLoading());
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {

            if (Engine.GameForm.Focused)
            {
                IsMouseVisible = false;


                //Geting Mouse and Keyboard stats
                MouseState mouse = Mouse.GetState();
                KeyboardState keyboard = Keyboard.GetState();

                // Update the mouse cursor.
                if (Engine.IsLoaded)
                    Engine.MouseCursor.Update(mouse, keyboard, gameTime);

                //Update scenemanager.

                //Scene.SceneManager.Update(mouseState, keyboardState, gameTime);
                sceneManager.Update(mouse, keyboard, gameTime);

                DbgScr.Update(mouse, keyboard, gameTime);
                base.Update(gameTime);

                // Update the sound engine.
                Audio.SongEngine.Update(mouse, keyboard, gameTime);
                Audio.SoundEffectEngine.Update(mouse, keyboard, gameTime);
            }
            else
            {

                IsMouseVisible = true;

            }
        }

        protected override void Draw(GameTime gameTime)
        {

            if (Engine.GameForm.Focused)
            {
                // Clear graphique device screen.
                GraphicsDevice.Clear(Color.CornflowerBlue);
                Engine.CurrentFrame++;

                // Update the debug frame counter.
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Core.EngineDebug.FrameCounter.Update(deltaTime);

                // Draw the scenemanager.

                //Scene.SceneManager.Draw(spriteBatch, gameTime);
                sceneManager.Draw(spriteBatch, gameTime);

                // Prepare the spritebatch.
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);


                // Show error message if something append on engine initialization.
                if (Core.Engine.AsErrore)
                {
                    spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), "Error Mode !", new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Alignment.Bottom, Style.Bold, Color.Red);
                }

                // Draw debug info.
                DbgScr.Draw(spriteBatch, gameTime);

                // Draw mouse cursor.
                if (Engine.IsLoaded)
                    Engine.MouseCursor.Draw(spriteBatch, gameTime);

                // Draw engine build info.
                if (Engine.engineConfig.Debug_DebugWaterMark)
                    spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), "Maker RiseEngine Build #" + Engine.Version.Revision + "\nLoaded plugin : " + Core.GameObjectsManager.LoadedAssemblies.Count, new Rectangle(16, 0, 256, 64), Alignment.Left, Style.DropShadow, Color.White);

                // End the sprite batch.
                spriteBatch.End();

            }
            else {

                spriteBatch.Begin();
                string text = "Le jeux est en pause.";
                Vector2 textSize = ContentEngine.SpriteFont("Engine", "segoeUI_16pt").MeasureString(text);
                spriteBatch.FillRectangle(new Rectangle(12, 12, (int)textSize.X + 8, (int)textSize.Y + 8), Color.Black);
                spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), text, new Vector2(16), Color.White);
                spriteBatch.End();

            }
                base.Draw(gameTime);

        }
    }
}
