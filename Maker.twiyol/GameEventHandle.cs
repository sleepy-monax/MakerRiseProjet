using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.twiyol
{
    public abstract class GameEventHandle
    {

        public abstract void OnWorldGeneration(Game.GameScene world);

    }
}
