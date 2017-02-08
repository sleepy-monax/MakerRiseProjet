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


        public GameInput(MouseState mouseState, MouseState oldMouseState, KeyboardState keyboardState, KeyboardState oldKeyboardState) {
            _mouseState       = mouseState;
            _oldMouseState    = oldMouseState;
            _keyboardState    = keyboardState;
            _oldKeyboardState = oldKeyboardState;
            MousePosition     = mouseState.Position;
        }

        // Input stats.
        private MouseState _mouseState;
        private MouseState _oldMouseState;
        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;

        public Point MousePosition;

        public bool IsKeyBoardKeyDown(Keys key) {
            return _keyboardState.IsKeyDown(key);
        }

        public bool IsKeyBoardKeyUp(Keys key)
        {
            return _keyboardState.IsKeyUp(key);
        }

        public bool IsKeyBoardKeyPress(Keys key) {
            return _oldKeyboardState.IsKeyDown(key) &&
                  _keyboardState.IsKeyUp(key);
        }

        public bool IsMouseKeyDown(MouseButton Button)
        {

            ButtonState ButtonStats = ButtonState.Released;

            switch (Button)
            {
                case MouseButton.Right:
                    ButtonStats = _mouseState.RightButton;
                    break;
                case MouseButton.Left:
                    ButtonStats = _mouseState.LeftButton;
                    break;
                case MouseButton.Middle:
                    ButtonStats = _mouseState.MiddleButton;
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
                    ButtonStats = _mouseState.RightButton;
                    OldButtonStats = _oldMouseState.RightButton;
                    break;
                case MouseButton.Left:
                    ButtonStats = _mouseState.LeftButton;
                    OldButtonStats = _oldMouseState.LeftButton;
                    break;
                case MouseButton.Middle:
                    ButtonStats = _mouseState.MiddleButton;
                    OldButtonStats = _oldMouseState.MiddleButton;
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
