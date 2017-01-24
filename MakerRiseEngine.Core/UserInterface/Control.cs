using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.UserInterface
{

    public enum MouseStats { Over, Down, None }
    public enum Anchor { TopLeft, TopCenter, TopRight, CenterLeft, Center, CenterRight, BottomLeft, BottomCenter, BottomRight }
    public enum Dock { Top, Bottom, Left, Right, Fill, None }

    public class ControlPadding
    {

        public int Up, Down, Left, Right = 0;

        public ControlPadding(int up, int down, int left, int right)
        {

            Up = up;
            Down = down;
            Left = left;
            Right = right;

        }

        public ControlPadding(int all)
        {
            Up = Down = Left = Right = all;
        }

        public ControlPadding()
        {

        }

        public Rectangle ToRectangle(Rectangle sourceRectangle)
        {
            return new Rectangle(sourceRectangle.X + Left, sourceRectangle.Y + Up,
                                 sourceRectangle.Width - Right - Left, sourceRectangle.Height - Down - Up);
        }

    }

    public abstract class Control : IDrawable
    {
        // Properties
        public bool Visible { get; set; } = true;
        public string Text { get; set; } = "Control";
        public Color ControlColor { get; set; } = Color.White;
        public Color TextColor { get; set; } = Color.White;
        public SpriteFont TextFont = ContentEngine.SpriteFont("Engine", "segoeUI_16pt");
        public ControlPadding Padding { get; set; } = new ControlPadding();
        public ControlPadding ChildMargin { get; set; } = new ControlPadding();

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
        public Anchor ControlAnchor { get; set; } = Anchor.TopLeft;
        public Dock ControlDock { get; set; } = Dock.None;

        // Child controls list.
        public List<Control> Childs = new List<Control>();

        public void AddChild(Control ChildControl)
        {
            ChildControl.ParrentControl = this;
            Childs.Add(ChildControl);
        }

        public void RemoveChild(Control ChildControl)
        {
            ChildControl.ParrentControl = null;
            Childs.Remove(ChildControl);
        }

        // Mouse Stats.
        public MouseStats mouseStats = MouseStats.None;

        // Interfacing.
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                // Draw the controls.
                OnDraw(spriteBatch, gameTime);

                // Draw debug.
                if (Engine.engineConfig.Debug_GuiFrame)
                {
                    spriteBatch.DrawRectangle(ControlRectangle, Color.Black);
                    if (Childs.Count > 0)
                    {
                        spriteBatch.FillRectangle(Padding.ToRectangle(ControlRectangle), new Color(Color.Blue, 0.0001f));
                    }
                }

                // Draw child controls.
                foreach (Control c in Childs)
                {
                    c.Draw(spriteBatch, gameTime);
                }
            }
        }

        public void Update(GameInput playerInput, GameTime gameTime)
        {
            Rectangle parrentRectangle = new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight);

            // Update Enchore.
            {
                // Get parrent controls rectangle.
                if (ParrentControl != null)
                {
                    parrentRectangle = ParrentControl.ControlRectangle;
                }

                // refresh controls location by anchor.
                Point newLocation = new Point(0, 0);

                switch (ControlAnchor)
                {
                    case Anchor.TopLeft:

                        newLocation = new Point(parrentRectangle.X + _Location.X, parrentRectangle.Y + _Location.Y);

                        break;
                    case Anchor.TopCenter:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width / 2 + _Location.X, parrentRectangle.Y + _Location.Y);

                        break;
                    case Anchor.TopRight:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + _Location.X, parrentRectangle.Y + _Location.Y);

                        break;
                    case Anchor.CenterLeft:

                        newLocation = new Point(parrentRectangle.X + _Location.X, parrentRectangle.Y + parrentRectangle.Height / 2 + _Location.Y);

                        break;
                    case Anchor.Center:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width / 2 + _Location.X, parrentRectangle.Y + parrentRectangle.Height / 2 + _Location.Y);

                        break;
                    case Anchor.CenterRight:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + _Location.X, parrentRectangle.Y + parrentRectangle.Height / 2 + _Location.Y);

                        break;
                    case Anchor.BottomLeft:

                        newLocation = new Point(parrentRectangle.X + _Location.X, parrentRectangle.Y + parrentRectangle.Height + _Location.Y);

                        break;
                    case Anchor.BottomCenter:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + _Location.X, parrentRectangle.Y + parrentRectangle.Height / 2 + _Location.Y);

                        break;
                    case Anchor.BottomRight:

                        newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + _Location.X, parrentRectangle.Y + parrentRectangle.Height + _Location.Y);

                        break;
                    default:
                        break;
                }

                _ControlRectangle = new Rectangle(newLocation, new Point(_ControlRectangle.Width, _ControlRectangle.Height));

            }

            // Update docks.
            {
                Rectangle childControlHost = Padding.ToRectangle(new Rectangle(new Point(0), _ControlRectangle.Size));

                foreach (Control c in this.Childs)
                {

                    if (c.Visible) {

                        switch (c.ControlDock)
                        {
                            case Dock.Top:

                                c.ControlRectangle = new Rectangle(childControlHost.Location, new Point(childControlHost.Width, c.ControlRectangle.Height));
                                childControlHost = new Rectangle(new Point(childControlHost.Location.X, childControlHost.Location.Y + c.ControlRectangle.Height + ChildMargin.Up), new Point(childControlHost.Width, childControlHost.Height - c.ControlRectangle.Height - ChildMargin.Up));

                                break;
                            case Dock.Bottom:

                                c.ControlRectangle = new Rectangle(childControlHost.Location.X, childControlHost.Location.Y + childControlHost.Height - c.ControlRectangle.Height , childControlHost.Width, c.ControlRectangle.Height);
                                childControlHost = new Rectangle(childControlHost.Location, new Point(childControlHost.Width, childControlHost.Height - c.ControlRectangle.Height - ChildMargin.Down));

                                break;
                            case Dock.Left:

                                c.ControlRectangle = new Rectangle(childControlHost.X, childControlHost.Y, c.ControlRectangle.Width, childControlHost.Height);
                                childControlHost = new Rectangle(childControlHost.X + c.ControlRectangle.Width + ChildMargin.Left, childControlHost.Y, childControlHost.Width - c.ControlRectangle.Width - ChildMargin.Left, childControlHost.Height);

                                break;
                            case Dock.Right:

                                c.ControlRectangle = new Rectangle(childControlHost.X + childControlHost.Width - c.ControlRectangle.Width, childControlHost.Y, c.ControlRectangle.Width, childControlHost.Height);
                                childControlHost = new Rectangle(childControlHost.Location, new Point(childControlHost.Width - c.ControlRectangle.Width - ChildMargin.Right, childControlHost.Height));

                                break;
                            case Dock.Fill:

                                c.ControlRectangle = new Rectangle(childControlHost.Location, childControlHost.Size);

                                break;
                        }
                    }   
                }
            }

            if (Visible)
            {
                // Update the controls.
                OnUpdate(playerInput, gameTime);

                // Update mouse.
                {


                    // Mouse is over the control?
                    if (ControlRectangle.Contains(playerInput.MousePosition))
                        mouseStats = MouseStats.Over;
                    else
                        mouseStats = MouseStats.None;

                    // Get if control containe the mouse pointer
                    if (mouseStats == MouseStats.Over)
                    {
                        mouseStats = playerInput.IsMouseKeyDown(MouseButton.Left) ? MouseStats.Down : MouseStats.Over;

                        // Recognize a single click of the left mouse button
                        if (playerInput.IsMouseClick(MouseButton.Left))
                        {
                            OnMouseClick();
                            onMouseClick?.Invoke();
                        }

                    }
                }

                // Update child controls.
                foreach (Control c in Childs)
                {
                    c.Update(playerInput, gameTime);
                }
            }
        }

        // Extentions.
        public void DrawSprite(SpriteBatch spritebatch, Sprite sprite, Rectangle rectangle, Color color, GameTime gameTime)
        {
            sprite.Draw(spritebatch, new Rectangle(ControlRectangle.X + rectangle.X, ControlRectangle.Y + rectangle.Y, rectangle.Size.X, rectangle.Size.Y), color, gameTime);
        }

        public void DrawT2D(SpriteBatch spritebatch, Texture2D texture2D, Rectangle rectangle, Color color)
        {

            spritebatch.Draw(texture2D, new Rectangle(ControlRectangle.X + rectangle.X, ControlRectangle.Y + rectangle.Y, rectangle.Size.X, rectangle.Size.Y), color);

        }

        public void DrawText(SpriteBatch spritebatch, SpriteFont font, string text, Rectangle rectangle, Color color, Alignment align = Alignment.Left, Style style = Style.Regular)
        {
            SpriteFontDraw.DrawString(spritebatch, font, text, new Rectangle(ControlRectangle.X + rectangle.X, ControlRectangle.Y + rectangle.Y, rectangle.Size.X, rectangle.Size.Y), align, style, color);
        }

        // Events.
        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) { }
        public virtual void OnUpdate(GameInput playerInput, GameTime gameTime) { }

        // Mouse Event.
        public virtual void OnMouseClick() { }

        // Declare Envent Handeling
        public delegate void ClickEventHandler();
        public event ClickEventHandler onMouseClick;
    }
}
