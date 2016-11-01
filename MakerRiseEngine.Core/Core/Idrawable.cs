using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiseEngine.Core
{
    
    public interface Idrawable
    {

        void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
            
    }
}
