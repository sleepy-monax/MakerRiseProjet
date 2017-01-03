using Microsoft.Xna.Framework;

namespace Maker.twiyol.AI
{
    public interface IAction : GameObject.IGameObject
    {
        void Performe(GameObject.Event.GameObjectEventArgs e, GameTime gametime);
    }
}
