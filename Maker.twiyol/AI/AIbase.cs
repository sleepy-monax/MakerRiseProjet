using Maker.RiseEngine.Core.GameObjects;
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
            if (e.ParrentEntity.Tags.GetTag("ai_action", -1) != -1) 
                GameComponentManager.GetGameObject<IAction>(e.ParrentEntity.Tags.GetTag("ai_action", -1)).Performe(e, gametime);
        }
    }
}
