
using Maker.RiseEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.EngineDebug
{
    class DebugOverlay
    {
        GameEngine Engine;
        double fpsCount;
        SpriteFont NormalFont;
        List<int> KeyDown = new List<int>();

        public DebugOverlay(GameEngine engine) {
            Engine = engine;
            NormalFont = Engine.ressourceManager.GetSpriteFont("Engine", "segoeUI_16pt");
        }

        public void Update(GameInput playerInput, GameTime gameTime)
        {
            fpsCount = Math.Round(FrameCounter.CurrentFramesPerSecond, MidpointRounding.AwayFromZero);
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
            if (Engine.userConfig.DebugShowFrameCounter)
            {
                spriteBatch.DrawString(NormalFont, "FPS : " + fpsCount, new Vector2(Engine.graphicsDeviceManager.PreferredBackBufferWidth - NormalFont.MeasureString("FPS : " + fpsCount).X - 16, 16), Color.White);

            }

            int index = 0;
            foreach (Keys key in KeyDown)
            {
                spriteBatch.DrawString(NormalFont, key.ToString(), new Vector2(16, index * NormalFont.MeasureString("O").Y), Color.White);

                index++;
            }
        }

    }
}
