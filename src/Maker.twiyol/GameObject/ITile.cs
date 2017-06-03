using Microsoft.Xna.Framework;

namespace Maker.Twiyol.GameObject
{
    public interface ITile : IWorldGameObject
    {

        Color MapColor { get; set; }
        void OnEntityWalkIn(Event.GameObjectEventArgs e, GameTime gametime);

    }
}
