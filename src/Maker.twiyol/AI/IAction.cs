using Maker.RiseEngine.GameObjects;
using Maker.Twiyol.GameObject.Event;
using Microsoft.Xna.Framework;

namespace Maker.Twiyol.AI
{
    public interface IAction : IGameObject
    {
        void Performe(GameObjectEventArgs e, GameTime gametime);
    }
}
