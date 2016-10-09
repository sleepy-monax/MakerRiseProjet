using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiseEngine.Core
{
    
    public class BaseObject
    {
        public virtual void Initialize(){}
        public virtual void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime){}
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime){
            
        }

    }
}
