using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.twiyol.AI
{
    public class AIbase
    {
        public virtual void Tick(GameObject.Event.GameObjectEventArgs e, GameInput playerInput, GameTime _GameTime)
        {
            //do nothing

        }

        public void ExecuteAction(GameObject.Event.GameObjectEventArgs e, GameTime gametime)
        {
            if (e.ParrentEntity.Action != -1) 
                GameObjectManager.GetGameObject<IAction>(e.ParrentEntity.Action).Performe(e, gametime);
        }
    }
}
