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

        //declare Envent Handeling
        public delegate void ClickEventHandler();
        public event ClickEventHandler OnMouseClick;
        SoundEffectColection SE = SoundEffectParser.Parse("Engine", "ButtonClick");

        public bool MouseOver = false;
        public bool MouseDown = false;



        public virtual void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int ContainerX, int ContainerY)
        {
            ClickRect = new Rectangle(SizeBox.X + ContainerX, SizeBox.Y + ContainerY, SizeBox.Width, SizeBox.Height);
            // The active state from the last frame is now old
            lastMouseState = currentMouseState;

            // Get the mouse state relevant for this frame
            currentMouseState = Mouse;

            // Get if control containe the mouse pointer
            if (ClickRect.Contains(Mouse.Position))
            {

                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    MouseDown = true;
                }
                else
                {
                    MouseDown = false;
                }

                MouseOver = true;
                // Recognize a single click of the left mouse button
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    Audio.SoundEffectEngine.PlaySoundEffects(SE);
                    OnMouseClick?.Invoke();
                }

            }
            else
            {

                MouseDown = false;
                MouseOver = false;

            }


        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {

            //doNothing();

        }

    }
}
