using Microsoft.Xna.Framework;

namespace Maker.RiseEngine.Core.AI
{
    public interface IAction : GameObject.IGameObject
    {
        void Performe(GameObject.Event.GameObjectEventArgs e, GameTime gametime);
    }
}
