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
        
        public RiseGame(bool _Glmode)
        {
            graphics = new GraphicsDeviceManager(this);
            Engine.graphics = graphics;
            Content.RootDirectory = "Data";
            Engine.MainGame = this;
            Engine.Window = Window;
            Engine.GameForm = (Form)Control.FromHandle(Window.Handle);
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
            
            // Hide the systeme mouse cursor.
            this.IsMouseVisible = false;

            base.Initialize();
        }



        protected override void LoadContent()
        {
            Core.EngineDebug.DebugLogs.WriteInLogs("LoadContent...", Core.EngineDebug.LogType.Info, "Core");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Core.Engine.GraphicsDevice = this.GraphicsDevice;
            Core.ContentEngine.Content = this.Content;
            Core.Rendering.SpriteSheets.CommonSheets.Load();

            DbgScr = new Core.EngineDebug.DebugScreen();
            Core.Scene.SceneManager.Initialize();
            Core.Scene.SceneManager.CurrentScene = 2;
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            //Geting Mouse and Keyboard stats
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            // Update the mouse cursor.
            if (Engine.IsLoaded)
                Engine.MouseCursor.Update(mouseState, keyboardState, gameTime);

            //Update scenemanager.
            Core.Scene.SceneManager.Update(mouseState, keyboardState, gameTime);
            DbgScr.Update(mouseState, keyboardState, gameTime);
            base.Update(gameTime);

            // Update the sound engine.
            Core.Audio.SongEngine.Update(mouseState, keyboardState, gameTime);
            Core.Audio.SoundEffectEngine.Update(mouseState, keyboardState, gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear graphique device screen.
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Engine.CurrentFrame++;
            
            // Update the debug frame counter.
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Core.EngineDebug.FrameCounter.Update(deltaTime);
            
            // Prepare the spritebatch.
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            // Draw the scenemanager.
            Core.Scene.SceneManager.Draw(spriteBatch, gameTime);

            // Show error message if something append on engine initialization.
            if (Core.Engine.AsErrore)
            {
                spriteBatch.DrawString(Core.ContentEngine.SpriteFont("Engine", "Consolas_16pt"), "Error Mode !", new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Alignment.Bottom, Style.Bold, Color.Red);
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

            base.Draw(gameTime);

        }
    }
}
