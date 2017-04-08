using Maker.RiseEngine.Core.GameObjects;
using Microsoft.Xna.Framework;

namespace Maker.twiyol.AI
{
    public interface IAction : RiseEngine.Core.GameObjects.IGameObject
    {
        void Performe(GameObject.Event.GameObjectEventArgs e, GameTime gametime);
    }
}
