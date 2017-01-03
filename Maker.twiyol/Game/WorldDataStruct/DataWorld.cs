using System;
using System.Collections.Generic;

namespace Maker.twiyol.Game.WorldDataStruct
{
    [Serializable]
    public class DataWorld
    {

        public DataChunk[,] chunks;
        public Dictionary<int, DataRegion> regions;

        public DataWorld()
        {
            regions = new Dictionary<int, DataRegion>();
        }

    }
}
