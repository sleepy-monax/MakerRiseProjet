using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.AI
{
    public interface IAction : GameObject.IGameObject
    {
        void Performe(GameObject.Event.GameObjectEventArgs e, GameTime gametime);
    }
}
