using Maker.Twiyol.Game;
using Maker.Twiyol.Game.WorldDataStruct;
using Maker.Twiyol.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Twiyol.Events
{
    public class WorldEventArgs : EventArgs
    {
        public DataWorld World;
        public WorldGenerator WorldGenerator;

        public WorldEventArgs(DataWorld world, WorldGenerator worldGenerator) {
            World = world;
            WorldGenerator = worldGenerator;
        }

    }
}
