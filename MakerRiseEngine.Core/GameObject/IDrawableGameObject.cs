using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Core.GameObject
{
    interface IDrawableGameObject : IGameObject
    {

        void OnUpdate(GameInput playerInput, GameTime gametime);
        void OnDraw(SpriteBatch spritebatch, GameTime gametime);

    }
}
