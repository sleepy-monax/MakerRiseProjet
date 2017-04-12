using Maker.twiyol.Game;
using Maker.twiyol.Game.WorldDataStruct;
using Maker.twiyol.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.twiyol.Events
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
