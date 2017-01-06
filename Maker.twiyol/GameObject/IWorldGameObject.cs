using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.twiyol.GameObject
{
    public interface IWorldGameObject : IGameObject
    {

        int MaxVariantCount { get; set; }

        void OnTick(Event.GameObjectEventArgs e, GameTime gametime);
        void OnUpdate(Event.GameObjectEventArgs e, PlayerInput playerInput, GameTime gametime);
        void OnDraw(Event.GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime);


    }
}
