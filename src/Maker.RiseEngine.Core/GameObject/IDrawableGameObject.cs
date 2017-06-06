using Maker.RiseEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.GameObjects
{
    interface IDrawableGameComponent : IGameObject
    {

        void OnUpdate(GameInput playerInput, GameTime gametime);
        void OnDraw(SpriteBatch spritebatch, GameTime gametime);

    }
}
