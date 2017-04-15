using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.EngineDebug.EngineConsole;
using Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands.Plugin;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Scenes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Maker.RiseEngine.Core.Audio;

namespace Maker.RiseEngine.Core
{
    public class engine : Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;
        debugScreen DebugScreen;
        public EngineConsole DebugConsole;

        public SceneManager SCENES;
        public RessourcesManager RESSOUCES;
        public SongsManager SONGS;

        public engine()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Plugins";
            
            // setup game engine.
            rise.graphics = Graphics;
            rise.ENGINE = this;
            rise.Window = Window;
            rise.GameForm = (Form)Control.FromHandle(Window.Handle);
            rise.GameForm.ResizeEnd += GameForm_ResizeEnd;
        }

        private void GameForm_ResizeEnd(object sender, System.EventArgs e)
        {
            // Set window setting.
            rise.graphics.PreferredBackBufferWidth = rise.Window.ClientBounds.Width;
            rise.graphics.PreferredBackBufferHeight = rise.Window.ClientBounds.Height;
            rise.graphics.ApplyChanges();

            GameConsoleOptions.Options.Height = rise.Window.ClientBounds.Height;
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

            DebugLogs.WriteLog("Rise!Engine version:" + rise.Version.ToString(), LogType.Info, "Core");
            
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

            rise.GraphicsDevice = GraphicsDevice;

            RESSOUCES = new RessourcesManager(Content);
            SCENES = new SceneManager(this);
            SONGS = new SongsManager(this);

            Rendering.SpriteSheets.CommonSheets.Load(RESSOUCES);

            DebugScreen = new debugScreen();

            // Setup DebugConsole.
            DebugConsole = new EngineConsole(spriteBatch, new GameConsoleOptions
            {
                ToggleKey = (int)Microsoft.Xna.Framework.Input.Keys.F12,
                Font = RESSOUCES.SpriteFont("Engine", "Consolas_16pt"),
                FontColor = Color.Gold,
                Prompt = ">",
                PromptColor = Color.Gold,
                CursorColor = Color.White,
                BackgroundColor = new Color(Color.Black, 100),
                PastCommandOutputColor = Color.White,
                BufferColor = Color.Gold
            }, this);

            DebugConsole.AddCommand("ping", a =>
            {
                // TODO your logic
                return String.Format("pong");
            });

            // Add engine commande in the terminal.
            DebugConsole.AddCommand(new PlugCommand());
            DebugConsole.AddCommand(new PlugListCommand());
            DebugConsole.AddCommand(new PlugInfoCommand());

            // Show the loading scene.
            SCENES.AddScene(new Scenes.Scenes.EngineLoading());
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

            if (rise.GameForm.Focused)
            {
                // Creating player input data structure.
                GameInput playerinput = new GameInput(mouseState, oldMouseState, keyboardState, oldKeyBoardState);

                // Update debug Console.
                DebugConsole.Update(playerinput, gameTime);

                // Pause game when console is open.
                if (!DebugConsole.IsOpen) {

                    //Update scenemanager.
                    SCENES.Update(playerinput, gameTime);

                    DebugScreen.Update(playerinput, gameTime);

                    // Update the sound engine.
                    SONGS.Update(mouseState, keyboardState, gameTime);
                    SoundEffectEngine.Update(mouseState, keyboardState, gameTime);

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
            if (rise.GameForm.Focused)
            {
                // Clear graphique device screen.
                GraphicsDevice.Clear(Color.CornflowerBlue);
                rise.CurrentFrame++;

                // Update the debug frame counter.
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                FrameCounter.Update(deltaTime);

                // Draw the scenemanager.
                SCENES.Draw(spriteBatch, gameTime);

                // Prepare the spritebatch.
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);


                // Show error message if something append on engine initialization.
                if (Core.rise.AsErrore)
                {
                    spriteBatch.DrawString(RESSOUCES.SpriteFont("Engine", "Consolas_16pt"), "Error Mode !", new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Alignment.Bottom, Style.Bold, Color.Red);
                }

                // Draw debug info.
                DebugScreen.Draw(spriteBatch, gameTime);

                // Draw engine build info.
                if (rise.engineConfig.Debug_DebugWaterMark)
                    spriteBatch.DrawString(RESSOUCES.SpriteFont("Engine", "Consolas_16pt"), "Maker RiseEngine Build #" + rise.Version.Revision, new Rectangle(24, 16, 256, 32), Alignment.Left, Style.DropShadow, Color.White);

                // End the sprite batch.
                spriteBatch.End();

                // Draw debugConsole.
                DebugConsole.Draw(spriteBatch, gameTime);
            }
            else
            {

                // Draw pause indicator.
                spriteBatch.Begin();
                string text = "Le jeux est en pause.";
                spriteBatch.DrawString(RESSOUCES.SpriteFont("Engine", "segoeUI_16pt"), text, new Rectangle(16, 16, 256, 64), Alignment.Center, Style.rectangle, Color.White);
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
