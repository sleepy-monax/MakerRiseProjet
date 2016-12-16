using Maker.RiseEngine.Core.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.UI.Controls
{
    public class Control
    {
        public MouseState lastMouseState, currentMouseState;
        public Rectangle SizeBox;
        public Rectangle ClickRect;

        // Declare Envent Handeling
        public delegate void ClickEventHandler();
        public event ClickEventHandler OnMouseClick;
        
        // Mouse click sound effect.
        SoundEffectColection SE = SoundEffectParser.Parse("Engine", "ButtonClick");

        // Mouse stats.
        public bool MouseOver = false;
        public bool MouseDown = false;
        
        public virtual void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int ContainerX, int ContainerY)
        {
            // Update mouse clic hitbox.
            ClickRect = new Rectangle(SizeBox.X + ContainerX, SizeBox.Y + ContainerY, SizeBox.Width, SizeBox.Height);
            
            // The active state from the last frame is now old
            lastMouseState = currentMouseState;

            // Get the mouse state relevant for this frame
            currentMouseState = Mouse;
            
            // Mouse is over the control?
            MouseOver = ClickRect.Contains(Mouse.Position);
            
            // Get if control containe the mouse pointer
            if (MouseOver)
            {
                MouseDown = (currentMouseState.LeftButton == ButtonState.Pressed);
                
                // Recognize a single click of the left mouse button
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    Audio.SoundEffectEngine.PlaySoundEffects(SE);
                    OnMouseClick?.Invoke();
                }
            }
            else
                MouseDown = false;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {}
    }
}
