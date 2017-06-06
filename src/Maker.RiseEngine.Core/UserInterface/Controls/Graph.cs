
using Maker.RiseEngine.Rendering;
using Maker.RiseEngine.Rendering.SpriteSheets;
using Maker.RiseEngine.Ressources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.UserInterface.Controls
{
    public class Graph : Control
    {

        Sprite panelCenter = Common.UserInterface.GetSprite("BoxC");

        Sprite panelUpLeft = Common.UserInterface.GetSprite("BoxUL");
        Sprite panelDownLeft = Common.UserInterface.GetSprite("BoxDL");

        Sprite panelUpRight = Common.UserInterface.GetSprite("BoxUR");
        Sprite panelDownRight = Common.UserInterface.GetSprite("BoxDR");

        Sprite panelMidUp = Common.UserInterface.GetSprite("BoxMU");
        Sprite panelMidDown = Common.UserInterface.GetSprite("BoxMD");
        Sprite panelMidLeft = Common.UserInterface.GetSprite("BoxML");
        Sprite panelMidRight = Common.UserInterface.GetSprite("BoxMR");

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

            Bound = rect;
            ControlColor = color;
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw center.
            DrawSprite(spriteBatch, panelCenter, new Rectangle(new Point(64), Bound.Size - new Point(128)), ControlColor, gameTime);

            // Draw corners.
            DrawSprite(spriteBatch, panelUpLeft, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelDownLeft, new Rectangle(0, Bound.Height - 64, 64, 64), ControlColor, gameTime);

            DrawSprite(spriteBatch, panelUpRight, new Rectangle(Bound.Width - 64, 0, 64, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelDownRight, new Rectangle(Bound.Width - 64, Bound.Height - 64, 64, 64), ControlColor, gameTime);

            // Draw edges.
            DrawSprite(spriteBatch, panelMidLeft, new Rectangle(0, 64, 64, Bound.Height - 128), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelMidRight, new Rectangle(Bound.Width - 64, 64, 64, Bound.Height - 128), ControlColor, gameTime);

            DrawSprite(spriteBatch, panelMidUp, new Rectangle(64, 0, Bound.Width - 128, 64), ControlColor, gameTime);
            DrawSprite(spriteBatch, panelMidDown, new Rectangle(64, Bound.Height - 64, Bound.Width - 128, 64), ControlColor, gameTime);

            var index = 0;
            foreach (var sample in DataSet)
            {
                spriteBatch.DrawLine(new Vector2(Bound.X + index, Bound.Y - Bound.Height - sample * 10), new Vector2(Bound.X + index, Bound.Y + Bound.Height), Color.Green);
                index++;
            }

            Vector2 averageLocation = new Vector2(Bound.X, Bound.Y - Bound.Height - AverageValue * 10);
            spriteBatch.DrawLine(averageLocation, averageLocation + new Vector2(Bound.Width, 0), Color.Red);

            DrawText(spriteBatch, Rise.Engine.ressourceManager.GetSpriteFont("Engine", "segoeUI_16pt"), Text, new Rectangle(new Point(16), Bound.Size), TextColor);
        }
    }
}
