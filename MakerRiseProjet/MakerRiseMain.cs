using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiseEngine.Core.UI;
using RiseEngine.Core;
using System.Windows.Forms;

namespace RiseEngine
{


    public class RiseGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Core.Debug.DebugScreen DbgScr;

        bool GLmode = false;

        public RiseGame(bool _Glmode)
        {

            graphics = new GraphicsDeviceManager(this);
            Core.Common.graphics = graphics;
            Content.RootDirectory = "Data";
            Core.Common.MainGame = this;
            Core.Common.Window = Window;
            Core.Common.GameForm = (Form)System.Windows.Forms.Control.FromHandle(Window.Handle);
            GLmode = _Glmode;

            

        }

        protected override void Initialize()
        {
            Core.Debug.Logs.Write("[Core] Initializing game engine...", Core.Debug.LogType.Info);

            Core.Config.Controls.Load();
            Core.Config.Debug.Load();



            //setting up screen
            if (Core.Config.Gfx.FullScreen == true) {
                graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
                graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            } else {
                graphics.PreferredBackBufferWidth = 1366;
                graphics.PreferredBackBufferHeight = 768;

                graphics.ApplyChanges();
            }



            

            Window.Title = "Rise : Le monde est votre seule limite.";
            Window.AllowAltF4 = false;
            Window.AllowUserResizing = false;
            Window.IsBorderless = false;

            this.IsMouseVisible = false;

            

            base.Initialize();
        }



        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Core.Debug.Logs.Write("[Core] LoadContent...", Core.Debug.LogType.Info);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Core.Common.GraphicsDevice = this.GraphicsDevice;
            Core.ContentEngine.Content = this.Content;
            Core.Rendering.SpriteSheets.CommonSheets.Load();

            DbgScr = new Core.Debug.DebugScreen();
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

            if (Engine.IsLoaded)
            Engine.MouseCursor.Update(mouseState, keyboardState, gameTime);

            Core.Scene.SceneManager.Update(mouseState, keyboardState, gameTime);
            DbgScr.Update(mouseState, keyboardState, gameTime);
            base.Update(gameTime);

            Core.Audio.SongEngine.Update(mouseState, keyboardState, gameTime);
            Core.Audio.SoundEffectEngine.Update(mouseState, keyboardState, gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            Engine.CurrentFrame++;
            //Debug Framecounter
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Core.Debug.FrameCounter.Update(deltaTime);

            //clear screen
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //draw
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            //drawGame
            Core.Scene.SceneManager.Draw(spriteBatch, gameTime);
            //draw Debug
            if (Core.Engine.AsErrore) {
                spriteBatch.DrawString(Core.ContentEngine.SpriteFont("Engine", "Consolas_16pt"), "Error Mode !", new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), helper.Alignment.Bottom, helper.Style.Bold, Color.Red);
            }

            DbgScr.Draw(spriteBatch, gameTime);

            if (Engine.IsLoaded)
                Engine.MouseCursor.Draw(spriteBatch, gameTime);

            if (Core.Config.Debug.DebugWaterMark)
                spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), "Maker RiseEngine Build #" + Engine.Version.Revision, new Rectangle(16,0,256,64), helper.Alignment.Left, helper.Style.DropShadow, Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);

        }

    }
}
