using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.GameScene
{
    public class GameScene : Idrawable
    {

        public virtual void Initialize() { }
        public virtual void UnloadScene() { }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        public virtual void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

        }
    }
}
