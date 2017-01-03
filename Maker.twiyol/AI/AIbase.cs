using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.twiyol.AI
{
    public class AIbase
    {
        public virtual void Tick(GameObject.Event.GameObjectEventArgs e, KeyboardState _KeyBoard, MouseState _Mouse, GameTime _GameTime)
        {
            //do nothing

        }

        public void ExecuteAction(GameObject.Event.GameObjectEventArgs e, GameTime gametime)
        {
            if (e.ParrentEntity.Action != -1) 
                GameObjectsManager.GetGameObject<IAction>(e.ParrentEntity.Action).Performe(e, gametime);
        }
    }
}
