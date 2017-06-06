using Maker.RiseEngine.Audio;
using Maker.RiseEngine.Config;
using Maker.RiseEngine.EngineDebug;
using Maker.RiseEngine.Input;
using Maker.RiseEngine.Plugin;
using Maker.RiseEngine.Rendering;
using Maker.RiseEngine.Rendering.SpriteSheets;
using Maker.RiseEngine.Scenes;
using Maker.RiseEngine.Scenes.Scenes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Maker.RiseEngine
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
        public Form gameForm;
        public Dictionary<string, IPlugin> loadedPlugins;
        public Version version = new Version(1, 0, 0);

        public GraphicsDeviceManager graphicsDeviceManager;
        public RessourcesManager     ressourceManager;
        public SceneManager          sceneManager;
        public SongManager           songManager;
        public SoundEffectManager    soundEffectManager;
        public int CurrentFrame = 0;

        public GameEngine()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Plugins";
        
            Rise.Engine = this;
            gameForm = (Form)Control.FromHandle(Window.Handle);
            gameForm.ResizeEnd += GameForm_ResizeEnd;
        }

        private void GameForm_ResizeEnd(object sender, System.EventArgs e)
        {
            // Set window setting.
            graphicsDeviceManager.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphicsDeviceManager.ApplyChanges();
        }

        protected override void Initialize()
        {
            Debug.WriteLog("Initializing 'Rise!Engine'", LogType.Info, nameof(GameEngine));

            // Show a very cool logo in terminal :)
            Debug.WriteLog("  ____  _          _ _____             _            ", LogType.Info, nameof(GameEngine));
            Debug.WriteLog(" |  _ \\(_)___  ___| | ____|_ __   __ _(_)_ __   ___ ", LogType.Info, nameof(GameEngine));
            Debug.WriteLog(" | |_) | / __|/ _ \\ |  _| | '_ \\ / _` | | '_ \\ / _ \\", LogType.Info, nameof(GameEngine));
            Debug.WriteLog(" |  _ <| \\__ \\  __/_| |___| | | | (_| | | | | |  __/", LogType.Info, nameof(GameEngine));
            Debug.WriteLog(" |_| \\_\\_|___/\\___(_)_____|_| |_|\\__, |_|_| |_|\\___|", LogType.Info, nameof(GameEngine));
            Debug.WriteLog("                                 |___/              ", LogType.Info, nameof(GameEngine));
            Debug.WriteLog(" ===================================================", LogType.Info, nameof(GameEngine));

            Debug.WriteLog("Rise!Engine version:" + version.ToString(), LogType.Info, nameof(GameEngine));

            // Set windows from property.
            Window.Title = "Rise : Le monde est votre seule limite";
            Window.AllowAltF4 = true;
            Window.AllowUserResizing = true;
            Window.IsBorderless = false;

            // Hide the systeme mouse cursor.
            IsMouseVisible = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Debug.WriteLog("LoadContent...", LogType.Info, nameof(GameEngine));
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ressourceManager = new RessourcesManager(this, Content);
            sceneManager = new SceneManager(this);
            songManager = new SongManager(this);
            soundEffectManager = new SoundEffectManager(this, 16);

            Ressources.Common.LoadCommonRessource(ressourceManager);
            debugOverlay = new DebugOverlay(this);
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

            if (gameForm.Focused)
            {
                // Creating player input data structure.
                GameInput playerinput = new GameInput(mouseState, oldMouseState, keyboardState, oldKeyBoardState);

                //Update scenemanager.
                sceneManager.Update(playerinput, gameTime);
                songManager.Update(gameTime);
                soundEffectManager.Update(gameTime);
                debugOverlay.Update(playerinput, gameTime);
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
            if (gameForm.Focused)
            {
                // Clear graphique device screen.
                GraphicsDevice.Clear(Color.CornflowerBlue);
                CurrentFrame++;

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
                    spriteBatch.DrawString(ressourceManager.GetSpriteFont("Engine", "Consolas_16pt"), "Maker RiseEngine Build #" + version.Revision, new Rectangle(24, 16, 256, 32), Alignment.Left, Style.DropShadow, Color.White);

                // End the sprite batch.
                spriteBatch.End();
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
