using Maker.RiseEngine.Core.GameObject;
using Microsoft.Xna.Framework;

namespace Maker.twiyol.AI
{
    public interface IAction : IGameObject
    {
        void Performe(GameObject.Event.GameObjectEventArgs e, GameTime gametime);
    }
}
