using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;


namespace Maker.RiseEngine.Core.UserInterface
{
    public enum MouseStats { Over, Down, None }

    public abstract class Control : Idrawable
    {
        // Properties
        bool Visible { get; set; } = true;
        bool enable { get; set; } = true;
        string Text { get; set; } = "Controls";
        public Rectangle ControlRectangle { get; set; } = new Rectangle(0, 0, 256, 64);

        // Child controls list.
        public List<Control> ChildControls = new List<Control>();

        // Mouse Stats.
        public MouseState lastMouseState, currentMouseState;
        public MouseStats mouseStats = MouseStats.None;

        // Interfacing.
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                // Draw the controls.
                onDraw(spriteBatch, gameTime);

                // Draw child controls.
                foreach (Control c in ChildControls)
                {
                    c.Draw(spriteBatch, gameTime);
                }
            }
        }

        public void Update(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            if (enable)
            {
                // Update the controls.
                onUpdate(mouse, keyBoard, gameTime);

                // Update mouse.
                {
                    // The active state from the last frame is now old
                    lastMouseState = currentMouseState;

                    // Get the mouse state relevant for this frame
                    currentMouseState = mouse;

                    // Mouse is over the control?
                    if (ControlRectangle.Contains(mouse.Position))
                        mouseStats = MouseStats.Over;
                    else
                        mouseStats = MouseStats.None;

                    // Get if control containe the mouse pointer
                    if (mouseStats == MouseStats.Over)
                    {
                        mouseStats = currentMouseState.LeftButton == ButtonState.Pressed ? MouseStats.Down : MouseStats.Over;

                        // Recognize a single click of the left mouse button
                        if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                        {
                            onMouseUp();
                            onMouseClick();
                        }

                        // Reconize mouse Down event.
                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Released)
                        {
                            onMouseDown();
                        }
                    }
                }

                // Update child controls.
                foreach (Control c in ChildControls)
                {
                    c.Update(mouse, keyBoard, gameTime);
                }
            }
        }

        // Extentions.
        public void DrawSprite(SpriteBatch spritebatch, Sprite sprite, Rectangle rectangle, Color color, GameTime gameTime)
        {
            sprite.Draw(spritebatch, new Rectangle(ControlRectangle.X + rectangle.X, ControlRectangle.Y + rectangle.Y, rectangle.Size.X, rectangle.Size.Y), color, gameTime);
        }

        public void DrawText(SpriteBatch spritebatch, SpriteFont font, string text, Rectangle rectangle, Color color, Rendering.SpriteFontDraw.Alignment align = Rendering.SpriteFontDraw.Alignment.Left, Rendering.SpriteFontDraw.Style style = Rendering.SpriteFontDraw.Style.Regular)
        {
            Rendering.SpriteFontDraw.DrawString(spritebatch, font, text, new Rectangle(ControlRectangle.X + rectangle.X, ControlRectangle.Y + rectangle.Y, rectangle.Size.X, rectangle.Size.Y), align, style, color);
        }

        // Events.
        public abstract void onDraw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void onUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime);

        // Mouse Event.
        public abstract void onMouseClick();
        public abstract void onMouseDown();
        public abstract void onMouseUp();
    }
}
