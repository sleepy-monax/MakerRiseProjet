using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.twiyol.GameObject
{
    public interface IWorldGameObject : IGameObject
    {

        int MaxVariantCount { get; set; }

        void OnTick(Event.GameObjectEventArgs e, GameTime gametime);
        void OnUpdate(Event.GameObjectEventArgs e, KeyboardState keyboard, MouseState mouse, GameTime gametime);
        void OnDraw(Event.GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime);


    }
}
