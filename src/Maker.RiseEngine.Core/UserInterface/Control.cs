
using Maker.RiseEngine.Input;
using Maker.RiseEngine.Rendering;
using Maker.RiseEngine.Rendering.SpriteSheets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Maker.RiseEngine.UserInterface
{
    public enum MouseStats { Over, Down, None }
    public enum Anchors { TopLeft, TopCenter, TopRight, CenterLeft, Center, CenterRight, BottomLeft, BottomCenter, BottomRight }
    public enum Docks { Top, Bottom, Left, Right, Fill, None }

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

        public ControlPadding() { }

        public Rectangle ToRectangle(Rectangle sourceRectangle)
        {
            return new Rectangle(sourceRectangle.X + Left, sourceRectangle.Y + Up,
                                 sourceRectangle.Width - Right - Left, sourceRectangle.Height - Down - Up);
        }
    }

    public abstract class Control
    {
        public bool           Visible { get; set; } = true;
        public string         Text { get; set; } = "Control";
        public Anchors        Anchor { get; set; } = Anchors.TopLeft;
        public Docks          Dock { get; set; } = Docks.None;
        public Color          ControlColor { get; set; } = Color.White;
        public Color          TextColor { get; set; } = Color.White;
        public SpriteFont     TextFont = Rise.Engine.ressourceManager.GetSpriteFont("Engine", "segoeUI_16pt");
        public ControlPadding Padding { get; set; } = new ControlPadding();
        public ControlPadding ChildMargin { get; set; } = new ControlPadding();
        public Control        ParrentControl = null;
        public MouseStats     MouseState = MouseStats.None;
        public List<Control>  ChildsControls = new List<Control>();
        public Rectangle      Bound { get { return BoundRectangle; } set { BoundLocation = value.Location; BoundRectangle = value; }}

        Point AnchorPoint;
        Rectangle BoundRectangle = new Rectangle(0, 0, 256, 64);
        Point BoundLocation;

        public virtual void OnDraw(SpriteBatch spriteBatch, GameTime gameTime) { }
        public virtual void OnUpdate(GameInput playerInput, GameTime gameTime) { }
        public virtual void OnMouseClick() { }

        public delegate void ClickEventHandler(Control sender);
        public event ClickEventHandler MouseClick;

        public void AddChild(Control ChildControl)
        {
            ChildControl.ParrentControl = this;
            ChildsControls.Add(ChildControl);
        }

        public void RemoveChild(Control ChildControl)
        {
            ChildControl.ParrentControl = null;
            ChildsControls.Remove(ChildControl);
        }

        public void RefreshLayout()
        {

            Rectangle parrentRectangle = ParrentControl != null ? ParrentControl.Bound : new Rectangle(0, 0, Rise.Engine.graphicsDeviceManager.PreferredBackBufferWidth, Rise.Engine.graphicsDeviceManager.PreferredBackBufferHeight);
            Point newLocation = new Point(0, 0);

            switch (Anchor)
            {
                case Anchors.TopLeft:
                    newLocation = new Point(parrentRectangle.X + BoundLocation.X, parrentRectangle.Y + BoundLocation.Y);
                    break;

                case Anchors.TopCenter:
                    newLocation = new Point(parrentRectangle.X + parrentRectangle.Width / 2 + BoundLocation.X, parrentRectangle.Y + BoundLocation.Y);
                    break;

                case Anchors.TopRight:
                    newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + BoundLocation.X, parrentRectangle.Y + BoundLocation.Y);
                    break;

                case Anchors.CenterLeft:
                    newLocation = new Point(parrentRectangle.X + BoundLocation.X, parrentRectangle.Y + parrentRectangle.Height / 2 + BoundLocation.Y);
                    break;

                case Anchors.Center:
                    newLocation = new Point(parrentRectangle.X + parrentRectangle.Width / 2 + BoundLocation.X, parrentRectangle.Y + parrentRectangle.Height / 2 + BoundLocation.Y);
                    break;

                case Anchors.CenterRight:
                    newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + BoundLocation.X, parrentRectangle.Y + parrentRectangle.Height / 2 + BoundLocation.Y);
                    break;

                case Anchors.BottomLeft:
                    newLocation = new Point(parrentRectangle.X + BoundLocation.X, parrentRectangle.Y + parrentRectangle.Height + BoundLocation.Y);
                    break;

                case Anchors.BottomCenter:
                    newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + BoundLocation.X, parrentRectangle.Y + parrentRectangle.Height / 2 + BoundLocation.Y);
                    break;

                case Anchors.BottomRight:
                    newLocation = new Point(parrentRectangle.X + parrentRectangle.Width + BoundLocation.X, parrentRectangle.Y + parrentRectangle.Height + BoundLocation.Y);
                    break;

                default:
                    break;
            }

            BoundRectangle = new Rectangle(newLocation, new Point(BoundRectangle.Width, BoundRectangle.Height));

            
            Rectangle childControlHost = Padding.ToRectangle(new Rectangle(new Point(0), BoundRectangle.Size));

            foreach (Control c in this.ChildsControls)
            if (c.Visible)
            switch (c.Dock)
            {
                case Docks.Top:
                    c.Bound = new Rectangle(childControlHost.Location, new Point(childControlHost.Width, c.Bound.Height));
                    childControlHost = new Rectangle(new Point(childControlHost.Location.X, childControlHost.Location.Y + c.Bound.Height + ChildMargin.Up), new Point(childControlHost.Width, childControlHost.Height - c.Bound.Height - ChildMargin.Up));
                    break;

                case Docks.Bottom:
                    c.Bound = new Rectangle(childControlHost.Location.X, childControlHost.Location.Y + childControlHost.Height - c.Bound.Height, childControlHost.Width, c.Bound.Height);
                    childControlHost = new Rectangle(childControlHost.Location, new Point(childControlHost.Width, childControlHost.Height - c.Bound.Height - ChildMargin.Down));
                    break;

                case Docks.Left:
                    c.Bound = new Rectangle(childControlHost.X, childControlHost.Y, c.Bound.Width, childControlHost.Height);
                    childControlHost = new Rectangle(childControlHost.X + c.Bound.Width + ChildMargin.Left, childControlHost.Y, childControlHost.Width - c.Bound.Width - ChildMargin.Left, childControlHost.Height);
                    break;

                case Docks.Right:
                    c.Bound = new Rectangle(childControlHost.X + childControlHost.Width - c.Bound.Width, childControlHost.Y, c.Bound.Width, childControlHost.Height);
                    childControlHost = new Rectangle(childControlHost.Location, new Point(childControlHost.Width - c.Bound.Width - ChildMargin.Right, childControlHost.Height));
                    break;

                case Docks.Fill:
                    c.Bound = new Rectangle(childControlHost.Location, childControlHost.Size);
                    break;
            }  
        }



        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            RefreshLayout();

            if (Visible)
            {
                // Draw the controls.
                OnDraw(spriteBatch, gameTime);

                // Draw debug.
                if (Rise.Engine.userConfig.DebugShowGuiFrame)
                {
                    spriteBatch.DrawRectangle(Bound, Color.Black);
                    if (ChildsControls.Count > 0)
                    {
                        spriteBatch.FillRectangle(Padding.ToRectangle(Bound), new Color(Color.Blue, 0.0001f));
                    }
                }

                // Draw child controls.
                foreach (Control c in ChildsControls)
                {
                    c.Draw(spriteBatch, gameTime);
                }
            }
        }

        public void Update(GameInput playerInput, GameTime gameTime)
        {
            if (Visible)
            {
                OnUpdate(playerInput, gameTime);

                if (Bound.Contains(playerInput.MousePosition)) MouseState = MouseStats.Over;
                else MouseState = MouseStats.None;

                if (MouseState == MouseStats.Over)
                {
                    MouseState = playerInput.IsMouseKeyDown(MouseButton.Left) ? MouseStats.Down : MouseStats.Over;

                    if (playerInput.IsMouseClick(MouseButton.Left))
                    {
                        OnMouseClick();
                        MouseClick?.Invoke(this);
                    }
                }
                
                foreach (Control c in ChildsControls)
                {
                    c.Update(playerInput, gameTime);
                }
            }
        }

        public void DrawSprite(SpriteBatch spritebatch, Sprite sprite, Rectangle rectangle, Color color, GameTime gameTime)
        {
            sprite.Draw(spritebatch, new Rectangle(Bound.X + rectangle.X, Bound.Y + rectangle.Y, rectangle.Size.X, rectangle.Size.Y), color, gameTime);
        }

        public void DrawTexture2D(SpriteBatch spritebatch, Texture2D texture2D, Rectangle rectangle, Color color)
        {
            spritebatch.Draw(texture2D, new Rectangle(Bound.X + rectangle.X, Bound.Y + rectangle.Y, rectangle.Size.X, rectangle.Size.Y), color);
        }

        public void DrawText(SpriteBatch spritebatch, SpriteFont font, string text, Rectangle rectangle, Color color, Alignment align = Alignment.Left, Style style = Style.Regular)
        {
            SpriteFontDraw.DrawString(spritebatch, font, text, new Rectangle(Bound.X + rectangle.X, Bound.Y + rectangle.Y, rectangle.Size.X, rectangle.Size.Y), align, style, color);
        }
    }
}
