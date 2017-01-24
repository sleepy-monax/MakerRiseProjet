using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Maker.RiseEngine.Core
{
    public class RiseEngine : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        debugScreen DbgScr;


        public Scenes.SceneManager sceneManager;

        public RiseEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            Engine.graphics = graphics;
            Content.RootDirectory = "Data";
            Engine.MainGame = this;
            Engine.Window = Window;
            Engine.GameForm = (Form)Control.FromHandle(Window.Handle);
            Engine.GameForm.ResizeEnd += GameForm_ResizeEnd;


            sceneManager = new Scenes.SceneManager(this);

        }

        private void GameForm_ResizeEnd(object sender, System.EventArgs e)
        {
            // Set window setting.
            Engine.graphics.PreferredBackBufferWidth = Engine.Window.ClientBounds.Width;
            Engine.graphics.PreferredBackBufferHeight = Engine.Window.ClientBounds.Height;
            Engine.graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            DebugLogs.WriteLog("Initializing 'Rise!Engine'", LogType.Info, "Core");

            DebugLogs.WriteLog("  ____  _          _ _____             _            ", LogType.Info, "Core");
            DebugLogs.WriteLog(" |  _ \\(_)___  ___| | ____|_ __   __ _(_)_ __   ___ ", LogType.Info, "Core");
            DebugLogs.WriteLog(" | |_) | / __|/ _ \\ |  _| | '_ \\ / _` | | '_ \\ / _ \\", LogType.Info, "Core");
            DebugLogs.WriteLog(" |  _ <| \\__ \\  __/_| |___| | | | (_| | | | | |  __/", LogType.Info, "Core");
            DebugLogs.WriteLog(" |_| \\_\\_|___/\\___(_)_____|_| |_|\\__, |_|_| |_|\\___|", LogType.Info, "Core");
            DebugLogs.WriteLog("                                 |___/              ", LogType.Info, "Core");

            DebugLogs.WriteLog(" ===================================================", LogType.Info, "Core");

            DebugLogs.WriteLog("Rise!Engine version:" + Engine.Version.ToString(), LogType.Info, "Core");
            // Set windows from property.
            Window.Title = "Rise : Le monde est votre seule limite";
            Window.AllowAltF4 = true;
            Window.AllowUserResizing = true;
            Window.IsBorderless = false;

            // Setup debug console.
            //System.Console.Title = "Maker Rise!Engine Debug Tool - " + Engine.Version.ToString();
            
            // Hide the systeme mouse cursor.
            IsMouseVisible = true;

            

            base.Initialize();
        }



        protected override void LoadContent()
        {
            DebugLogs.WriteLog("LoadContent...", LogType.Info, "Core");

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Engine.GraphicsDevice = GraphicsDevice;
            ContentEngine.Content = Content;
            Rendering.SpriteSheets.CommonSheets.Load();

            DbgScr = new debugScreen();

            // Show the loading scene.
            sceneManager.AddScene(new Scenes.Scenes.EngineLoading());
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        MouseState oldMouseState = new MouseState();
        KeyboardState oldKeyBoardState = new KeyboardState();

        protected override void Update(GameTime gameTime)
        {
            //Geting Mouse and Keyboard stats
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if (Engine.GameForm.Focused)
            {
                // Creating player input data structure.
                GameInput playerinput = new GameInput(mouseState, oldMouseState, keyboardState, oldKeyBoardState);
                
                //Update scenemanager.
                sceneManager.Update(playerinput, gameTime);

                DbgScr.Update(playerinput, gameTime);
                base.Update(gameTime);

                // Update the sound engine.
                Audio.SongEngine.Update(mouseState, keyboardState, gameTime);
                Audio.SoundEffectEngine.Update(mouseState, keyboardState, gameTime);
            }

            // Set old inputStats.
            oldMouseState = mouseState;
            oldKeyBoardState = keyboardState;
        }

        protected override void Draw(GameTime gameTime)
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            if (Engine.GameForm.Focused)
            {
                // Clear graphique device screen.
                GraphicsDevice.Clear(Color.CornflowerBlue);
                Engine.CurrentFrame++;

                // Update the debug frame counter.
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                FrameCounter.Update(deltaTime);

                // Draw the scenemanager.
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

                // Draw engine build info.
                if (Engine.engineConfig.Debug_DebugWaterMark)
                    spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), "Maker RiseEngine Build #" + Engine.Version.Revision, new Rectangle(16, 0, 256, 64), Alignment.Left, Style.DropShadow, Color.White);

                // End the sprite batch.
                spriteBatch.End();

            }
            else
            {

                // Draw pause indicator.
                spriteBatch.Begin();
                string text = "Le jeux est en pause.";
                spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), text, new Rectangle(16, 16, 256, 64), Alignment.Center, Style.rectangle, Color.White);
                spriteBatch.End();

            }
            base.Draw(gameTime);

            s.Stop();

            FrameCounter._sampleFrameTimeBuffer.Enqueue(s.ElapsedMilliseconds);
            if (FrameCounter._sampleFrameTimeBuffer.Count > FrameCounter.MAXIMUM_SAMPLES)
            {
                FrameCounter._sampleFrameTimeBuffer.Dequeue();
                FrameCounter.AverageFramesTime = FrameCounter._sampleFrameTimeBuffer.Average(i => i);
            }
            else
            {
                FrameCounter.AverageFramesTime = s.ElapsedMilliseconds;
            }

            
        }
    }
}
