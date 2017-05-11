using Microsoft.Xna.Framework;

namespace Maker.Twiyol.GameObject
{
    public interface ITile : IWorldGameObject
    {

        System.Drawing.Color MapColor { get; set; }

        void OnEntityWalkIn(Event.GameObjectEventArgs e, GameTime gametime);

    }
}
