using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.EngineDebug
{
    class debugScreen : IDrawable
    {
        double FPS;
        SpriteFont NormalFont = rise.ENGINE.RESSOUCES.SpriteFont("Engine", "segoeUI_16pt");
        List<int> KeyDown = new List<int>();

        public void Update(GameInput playerInput, GameTime gameTime)
        {
            //Get FPS value
            FPS = Math.Round(FrameCounter.CurrentFramesPerSecond, MidpointRounding.AwayFromZero);
            KeyDown.Clear();
            for (int i = 0; i < 256; i++)
            {
                if (playerInput.IsKeyBoardKeyDown((Keys)i)) {
                    KeyDown.Add(i);
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //Draw FPS values
            if (rise.engineConfig.Debug_FrameCounter)
            {
                spriteBatch.DrawString(NormalFont, "FPS : " + FPS, new Vector2(rise.graphics.PreferredBackBufferWidth - NormalFont.MeasureString("FPS : " + FPS).X - 16, 16), Color.White);

            }

            int index = 0;
            foreach (var item in KeyDown)
            {
                spriteBatch.DrawString(NormalFont, ((Keys)(item)).ToString(), new Vector2(16, index * NormalFont.MeasureString("O").Y), Color.White);

                index++;
            }
        }

    }
}
