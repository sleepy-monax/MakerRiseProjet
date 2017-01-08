using Maker.twiyol.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.twiyol.Events
{
    public class WorldEventArgs : EventArgs
    {
        public GameScene World;

        public WorldEventArgs(GameScene world) {
            World = world;
        }

    }
}
