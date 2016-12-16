using Maker.RiseEngine.Core.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.UI.Controls
{
    public enum MouseStats{Over, Down, None }

    public class Control
    {
        public MouseState lastMouseState, currentMouseState;
        public Rectangle SizeBox;
        public Rectangle ClickRect;

        // Declare Envent Handeling
        public delegate void ClickEventHandler();
        public event ClickEventHandler OnMouseClick;
        
        // Mouse click sound effect.
        SoundEffectColection SoundEffect = SoundEffectParser.Parse("Engine", "ButtonClick");

        // Mouse stats.
        public MouseStats mouseStats = MouseStats.None;
      
        public virtual void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int ContainerX, int ContainerY)
        {
            // Update mouse clic hitbox.
            ClickRect = new Rectangle(SizeBox.X + ContainerX, SizeBox.Y + ContainerY, SizeBox.Width, SizeBox.Height);
            
            // The active state from the last frame is now old
            lastMouseState = currentMouseState;

            // Get the mouse state relevant for this frame
            currentMouseState = Mouse;

            // Mouse is over the control?
            if (ClickRect.Contains(Mouse.Position))
                mouseStats = MouseStats.Over;
            else
                mouseStats = MouseStats.None;

            // Get if control containe the mouse pointer
            if (mouseStats == MouseStats.Over)
            {
                mouseStats  = currentMouseState.LeftButton == ButtonState.Pressed ? MouseStats.Down : MouseStats.Over;
                
                // Recognize a single click of the left mouse button
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    SoundEffectEngine.PlaySoundEffect(SoundEffect);
                    OnMouseClick?.Invoke();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {}
    }
}
