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
    public enum Anchor { UpLeft, Up, UpRight, Left, Center, Right, DownLeft, Down, DownRight }

    public abstract class Control : Idrawable
    {
        // Properties
        public bool Visible { get; set; } = true;
        public bool Enable { get; set; } = true;
        public string Text { get; set; } = "Controls";
        public Color ControlColor { get; set; } = Color.White;
        public Color TextColor { get; set; } = Color.White;

        public Control ParrentControl = null;

        // Style
        Rectangle _ControlRectangle = new Rectangle(0, 0, 256, 64);
        Point _Location;

        Point AnchorPoint;

        public Rectangle ControlRectangle
        {
            get { return _ControlRectangle; }
            set
            {
                _Location = value.Location;
                _ControlRectangle = value;
            }
        }
        public Anchor ControlAnchor { get; set; } = Anchor.UpLeft;

        // Child controls list.
        List<Control> ChildControls = new List<Control>();

        public void AddChild(Control child)
        {
            child.ParrentControl = this;
            ChildControls.Add(child);
        }

        public void RemoveChild(Control child)
        {
            child.ParrentControl = null;
            ChildControls.Remove(child);
        }

        // Mouse Stats.
        public MouseState lastMouseState, currentMouseState;
        public MouseStats mouseStats = MouseStats.None;

        // Interfacing.
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                // Draw the controls.
                OnDraw(spriteBatch, gameTime);

                // Draw child controls.
                foreach (Control c in ChildControls)
                {
                    c.Draw(spriteBatch, gameTime);
                }
            }
        }

        public void Update(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            // Update Enchore.
            {
                Rectangle parrentRectangle = new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight);

                // Get parrent controls rectangle.
                if (ParrentControl != null)
                {
                    parrentRectangle = ParrentControl.ControlRectangle;
                }

                // refresh controls location by anchor.
                Point newLocation = new Point(0, 0);

                switch (ControlAnchor)
                {
                    case Anchor.UpLeft:

                        newLocation = new Point(parrentRectangle.X + _Location.X, parrentRectangle.Y + _Location.Y);

                        break;
                    case Anchor.Up:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width / 2 + _Location.X, parrentRectangle.Y + _Location.Y);

                        break;
                    case Anchor.UpRight:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + _Location.X, parrentRectangle.Y + _Location.Y);

                        break;
                    case Anchor.Left:

                        newLocation = new Point(parrentRectangle.X + _Location.X, parrentRectangle.Y + parrentRectangle.Height / 2 + _Location.Y);

                        break;
                    case Anchor.Center:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width / 2 + _Location.X, parrentRectangle.Y + parrentRectangle.Height / 2 + _Location.Y);

                        break;
                    case Anchor.Right:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + _Location.X, parrentRectangle.Y + parrentRectangle.Height / 2 + _Location.Y);

                        break;
                    case Anchor.DownLeft:

                        newLocation = new Point(parrentRectangle.X + _Location.X, parrentRectangle.Y + parrentRectangle.Height + _Location.Y);

                        break;
                    case Anchor.Down:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + _Location.X, parrentRectangle.Y + parrentRectangle.Height / 2 + _Location.Y);

                        break;
                    case Anchor.DownRight:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + _Location.X, parrentRectangle.Y + parrentRectangle.Height + _Location.Y);

                        break;
                    default:
                        break;
                }

                _ControlRectangle = new Rectangle(newLocation, new Point(_ControlRectangle.Width, _ControlRectangle.Height));

            }

            if (Enable)
            {
                // Update the controls.
                OnUpdate(mouse, keyBoard, gameTime);

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
                            OnMouseUp();
                            OnMouseClick();
                            onMouseClick?.Invoke();
                        }

                        // Reconize mouse Down event.
                        if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Released)
                        {
                            OnMouseDown();
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
        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) { }
        public virtual void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime) { }

        // Mouse Event.
        public virtual void OnMouseClick() { }
        public virtual void OnMouseDown() { }
        public virtual void OnMouseUp() { }

        // Declare Envent Handeling
        public delegate void ClickEventHandler();
        public event ClickEventHandler onMouseClick;
    }
}
