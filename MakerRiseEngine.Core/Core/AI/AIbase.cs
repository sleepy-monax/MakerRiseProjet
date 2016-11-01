
using RiseEngine.Core.World.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.AI
{
    public class AIbase
    {

        public virtual void Tick(GameObject.Event.GameObjectEventArgs e, KeyboardState _KeyBoard, MouseState _Mouse, GameTime _GameTime)
        {
            //do nothing

        }



        public void ExecuteAction(GameObject.Event.GameObjectEventArgs e, GameTime gametime)
        {

            if (e.ParrentEntity.Action != -1) {

                GameObjectsManager.Actions[e.ParrentEntity.Action].Performe(e, gametime);

            }

        }

    }
}
