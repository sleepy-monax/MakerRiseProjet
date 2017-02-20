using Maker.RiseEngine.Core.GameComponent;
using Microsoft.Xna.Framework;

namespace Maker.twiyol.AI
{
    public interface IAction : RiseEngine.Core.GameComponent.IGameObject
    {
        void Performe(GameObject.Event.GameObjectEventArgs e, GameTime gametime);
    }
}
