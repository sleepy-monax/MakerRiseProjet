
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.UserInterface.Controls
{
    public class Graph : Control
    {

        Sprite panelCenter = CommonSheets.GUI.GetSprite("BoxC");

        Sprite panelUpLeft = CommonSheets.GUI.GetSprite("BoxUL");
        Sprite panelDownLeft = CommonSheets.GUI.GetSprite("BoxDL");

        Sprite panelUpRight = CommonSheets.GUI.GetSprite("BoxUR");
        Sprite panelDownRight = CommonSheets.GUI.GetSprite("BoxDR");

        Sprite panelMidUp = CommonSheets.GUI.GetSprite("BoxMU");
        Sprite panelMidDown = CommonSheets.GUI.GetSprite("BoxMD");
        Sprite panelMidLeft = CommonSheets.GUI.GetSprite("BoxML");
        Sprite panelMidRight = CommonSheets.GUI.GetSprite("BoxMR");

        private Queue<float> DataSet;
        private float AverageValue;

        public int MAXIMUM_SAMPLES { get; private set; }

        public void AddValue(float Value) {
            DataSet.Enqueue(Value);

            if (DataSet.Count > MAXIMUM_SAMPLES)
            {
                DataSet.Dequeue();
                AverageValue = DataSet.Average(i => i);
            }
            else
            {
                AverageValue = Value;
            }
        }

        public Graph(Rectangle rect, Color color)
        {
            DataSet = new Queue<float>();

            ControlRectangle = rect;
            ControlColor = color;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw center.
            DrawSprite(spriteBatch, panelCenter, new Rectangle(new Point(64), ControlRectangle.Size - new Point(128)), ControlColor, gameTime);

            // Draw corners.
            DrawSprite(spriteBatch, panelUpLeft, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelDownLeft, new Rectangle(0, ControlRectangle.Height - 64, 64, 64), ControlColor, gameTime);

            DrawSprite(spriteBatch, panelUpRight, new Rectangle(ControlRectangle.Width - 64, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelDownRight, new Rectangle(ControlRectangle.Width - 64, ControlRectangle.Height - 64, 64, 64), ControlColor, gameTime);

            // Draw edges.
            DrawSprite(spriteBatch, panelMidLeft, new Rectangle(0, 64, 64, ControlRectangle.Height - 128), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelMidRight, new Rectangle(ControlRectangle.Width - 64, 64, 64, ControlRectangle.Height - 128), ControlColor, gameTime);

            DrawSprite(spriteBatch, panelMidUp, new Rectangle(64, 0, ControlRectangle.Width - 128, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelMidDown, new Rectangle(64, ControlRectangle.Height - 64, ControlRectangle.Width - 128, 64), ControlColor, gameTime);

            var index = 0;
            foreach (var sample in DataSet)
            {
                spriteBatch.DrawLine(new Vector2(ControlRectangle.X + index, ControlRectangle.Y - ControlRectangle.Height - sample * 10), new Vector2(ControlRectangle.X + index, ControlRectangle.Y + ControlRectangle.Height), Color.Green);
                index++;
            }

            Vector2 averageLocation = new Vector2(ControlRectangle.X, ControlRectangle.Y - ControlRectangle.Height - AverageValue * 10);
            spriteBatch.DrawLine(averageLocation, averageLocation + new Vector2(ControlRectangle.Width, 0), Color.Red);

            DrawText(spriteBatch, rise.ENGINE.RESSOUCES.GetSpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(new Point(16), ControlRectangle.Size), TextColor);
        }
    }
}
