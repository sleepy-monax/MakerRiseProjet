using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Input
{

    public enum MouseButton {
        Right, Left, Middle
    }

    public class GameInput
    {

        public Point MousePosition;

        private MouseState CurrentMouseState;
        private MouseState OldMouseState;
        private KeyboardState CurrentKeyboardState;
        private KeyboardState OldKeyboardState;

        public GameInput(MouseState mouseState, MouseState oldMouseState, KeyboardState keyboardState, KeyboardState oldKeyboardState)
        {
            CurrentMouseState       = mouseState;
            OldMouseState    = oldMouseState;
            CurrentKeyboardState    = keyboardState;
            OldKeyboardState = oldKeyboardState;
            MousePosition     = mouseState.Position;
        }

        public bool IsKeyBoardKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyBoardKeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyBoardKeyPress(Keys key)
        {
            return OldKeyboardState.IsKeyDown(key) && CurrentKeyboardState.IsKeyUp(key);
        }

        public bool IsMouseKeyDown(MouseButton Button)
        {

            ButtonState ButtonStats = ButtonState.Released;

            switch (Button)
            {
                case MouseButton.Right:
                    ButtonStats = CurrentMouseState.RightButton;
                    break;
                case MouseButton.Left:
                    ButtonStats = CurrentMouseState.LeftButton;
                    break;
                case MouseButton.Middle:
                    ButtonStats = CurrentMouseState.MiddleButton;
                    break;
                default:
                    break;
            }

            if (ButtonStats == ButtonState.Pressed)
            {
                return true;
            }

            return false;

        }

        public bool IsMouseClick(MouseButton Button = MouseButton.Left) {

            ButtonState ButtonStats = ButtonState.Released;
            ButtonState OldButtonStats = ButtonState.Released;

            switch (Button)
            {
                case MouseButton.Right:
                    ButtonStats = CurrentMouseState.RightButton;
                    OldButtonStats = OldMouseState.RightButton;
                    break;
                case MouseButton.Left:
                    ButtonStats = CurrentMouseState.LeftButton;
                    OldButtonStats = OldMouseState.LeftButton;
                    break;
                case MouseButton.Middle:
                    ButtonStats = CurrentMouseState.MiddleButton;
                    OldButtonStats = OldMouseState.MiddleButton;
                    break;
                default:
                    break;
            }

            if (ButtonStats == ButtonState.Released && OldButtonStats == ButtonState.Pressed)
            {
                return true;
            }

            return false;

        }
    }
}
