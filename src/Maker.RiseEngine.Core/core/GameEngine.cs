using Maker.RiseEngine.Core.Audio;
using Maker.RiseEngine.Core.Config;
using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.EngineDebug.EngineConsole;
using Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands.Plugin;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Maker.RiseEngine.Core.Scenes;
using Maker.RiseEngine.Core.Scenes.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Maker.RiseEngine.Core
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var game = new GameEngine())
            {

                game.Run();

            }
        }
    }

    public class GameEngine : Game
    {
        SpriteBatch spriteBatch;
        DebugOverlay debugOverlay;
        public EngineUserConfig userConfig = new EngineUserConfig();
        public EngineConsole debugConsole;

        public GraphicsDeviceManager graphicsDeviceManager;
        public RessourcesManager     ressourceManager;
        public SceneManager          sceneManager;
        public SongManager           songManager;
        public SoundEffectManager    soundEffectManager;



        public GameEngine()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Plugins";
        
            Rise.Engine = this;
            Rise.Window = Window;
            Rise.GameForm = (Form)Control.FromHandle(Window.Handle);
            Rise.GameForm.ResizeEnd += GameForm_ResizeEnd;
        }

        private void GameForm_ResizeEnd(object sender, System.EventArgs e)
        {
            // Set window setting.
            graphicsDeviceManager.PreferredBackBufferWidth = Rise.Window.ClientBounds.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Rise.Window.ClientBounds.Height;
            graphicsDeviceManager.ApplyChanges();

            GameConsoleOptions.Options.Height = Rise.Window.ClientBounds.Height;
            //GameConsoleOptions.Options.Width = (int)(Engine.Window.ClientBounds.Width * 0.66f);
        }

        protected override void Initialize()
        {
            DebugLogs.WriteLog("Initializing 'Rise!Engine'", LogType.Info, "Core");

            // Show a very cool logo in terminal :)
            DebugLogs.WriteLog("  ____  _          _ _____             _            ", LogType.Info, "Core");
            DebugLogs.WriteLog(" |  _ \\(_)___  ___| | ____|_ __   __ _(_)_ __   ___ ", LogType.Info, "Core");
            DebugLogs.WriteLog(" | |_) | / __|/ _ \\ |  _| | '_ \\ / _` | | '_ \\ / _ \\", LogType.Info, "Core");
            DebugLogs.WriteLog(" |  _ <| \\__ \\  __/_| |___| | | | (_| | | | | |  __/", LogType.Info, "Core");
            DebugLogs.WriteLog(" |_| \\_\\_|___/\\___(_)_____|_| |_|\\__, |_|_| |_|\\___|", LogType.Info, "Core");
            DebugLogs.WriteLog("                                 |___/              ", LogType.Info, "Core");
            DebugLogs.WriteLog(" ===================================================", LogType.Info, "Core");

            DebugLogs.WriteLog("Rise!Engine version:" + Rise.Version.ToString(), LogType.Info, "Core");
            
            // Set windows from property.
            Window.Title = "Rise : Le monde est votre seule limite";
            Window.AllowAltF4 = true;
            Window.AllowUserResizing = true;
            Window.IsBorderless = false;

            // Setup debug console.
            // System.Console.Title = "Maker Rise!Engine Debug Tool - " + Engine.Version.ToString(); 
            // (Remove because crash when terminal is desactivated :/)
            
            // Hide the systeme mouse cursor.
            IsMouseVisible = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            DebugLogs.WriteLog("LoadContent...", LogType.Info, "Core");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ressourceManager = new RessourcesManager(this, Content);
            sceneManager = new SceneManager(this);
            songManager = new SongManager(this);
            soundEffectManager = new SoundEffectManager(this, 16);

            CommonSheets.Load(ressourceManager);

            debugOverlay = new DebugOverlay(this);

            // Setup DebugConsole.
            debugConsole = new EngineConsole(spriteBatch, new GameConsoleOptions
            {
                ToggleKey = (int)Microsoft.Xna.Framework.Input.Keys.F12,
                Font = ressourceManager.GetSpriteFont("Engine", "Consolas_16pt"),
                FontColor = Color.Gold,
                Prompt = ">",
                PromptColor = Color.Gold,
                CursorColor = Color.White,
                BackgroundColor = new Color(Color.Black, 100),
                PastCommandOutputColor = Color.White,
                BufferColor = Color.Gold
            }, this);

            debugConsole.AddCommand("ping", a =>
            {
                // TODO your logic
                return String.Format("pong");
            });

            // Add engine commande in the terminal.
            debugConsole.AddCommand(new PlugCommand());
            debugConsole.AddCommand(new PlugListCommand());
            debugConsole.AddCommand(new PlugInfoCommand());

            // Show the loading scene.
            sceneManager.AddScene(new EngineLoading());
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        // Use for detecting key Up Down.
        MouseState oldMouseState = new MouseState();
        KeyboardState oldKeyBoardState = new KeyboardState();

        protected override void Update(GameTime gameTime)
        {
            //Geting Mouse and Keyboard stats
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if (Rise.GameForm.Focused)
            {
                // Creating player input data structure.
                GameInput playerinput = new GameInput(mouseState, oldMouseState, keyboardState, oldKeyBoardState);

                // Update debug Console.
                debugConsole.Update(playerinput, gameTime);

                // Pause game when console is open.
                if (!debugConsole.IsOpen) {

                    //Update scenemanager.
                    sceneManager.Update(playerinput, gameTime);

                    debugOverlay.Update(playerinput, gameTime);

                    // Update the sound engine.
                    songManager.Update(gameTime);
                    soundEffectManager.Update(gameTime);

                }
            }

            // Set old inputStats.
            oldMouseState = mouseState;
            oldKeyBoardState = keyboardState;
            
            // Update base class.
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Setup stopwatch.
            Stopwatch s = new Stopwatch();
            s.Start();

            // Get if the game windows is focus and if is not focus pause the game.
            if (Rise.GameForm.Focused)
            {
                // Clear graphique device screen.
                GraphicsDevice.Clear(Color.CornflowerBlue);
                Rise.CurrentFrame++;

                // Update the debug frame counter.
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                FrameCounter.Update(deltaTime);

                // Draw the scenemanager.
                sceneManager.Draw(spriteBatch, gameTime);

                // Prepare the spritebatch.
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                // Draw debug info.
                debugOverlay.Draw(spriteBatch, gameTime);

                // Draw engine build info.
                if (userConfig.DebugShowDebugWaterMark)
                    spriteBatch.DrawString(ressourceManager.GetSpriteFont("Engine", "Consolas_16pt"), "Maker RiseEngine Build #" + Rise.Version.Revision, new Rectangle(24, 16, 256, 32), Alignment.Left, Style.DropShadow, Color.White);

                // End the sprite batch.
                spriteBatch.End();

                // Draw debugConsole.
                debugConsole.Draw(spriteBatch, gameTime);
            }
            else
            {

                // Draw pause indicator.
                spriteBatch.Begin();
                string text = "Le jeux est en pause.";
                spriteBatch.DrawString(ressourceManager.GetSpriteFont("Engine", "segoeUI_16pt"), text, new Rectangle(16, 16, 256, 64), Alignment.Center, Style.rectangle, Color.White);
                spriteBatch.End();

            }
            base.Draw(gameTime);

            s.Stop();

            // Update frame counter.
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
