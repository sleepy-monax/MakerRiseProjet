using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.EngineDebug
{
    class DebugScreen : Idrawable
    {
        double FPS;
        SpriteFont NormalFont = ContentEngine.SpriteFont("Engine", "segoeUI_16pt");

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            //Get FPS value
            FPS = Math.Round(EngineDebug.FrameCounter.CurrentFramesPerSecond, MidpointRounding.AwayFromZero);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //Draw FPS values
            if (Config.Debug.FrameCounter)
            {
                spriteBatch.DrawString(NormalFont, "FPS : " + FPS, new Vector2(Common.graphics.PreferredBackBufferWidth - NormalFont.MeasureString("FPS : " + FPS).X - 16, 16), Color.White);
            }
        }

    }
}
