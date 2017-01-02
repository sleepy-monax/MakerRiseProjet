using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Game.WorldDataStruct
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
