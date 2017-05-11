using System;
using System.Collections.Generic;
using System.Drawing;

namespace Maker.Twiyol.Game.WorldDataStruct
{
    [Serializable]
    public class DataWorld
    {

        public string Name;
        public int Seed;
        public int Size;

        public Bitmap WorldBitmap;

        public DataChunk[,] chunks;
        public Dictionary<int, DataRegion> regions;
        public DataCamera Camera;

        public DataEntity playerEntity;

        public DataWorld(string name, int seed, int size)
        {
            Name = name;
            Seed = seed;
            Size = size;
            regions = new Dictionary<int, DataRegion>();
            Camera = new DataCamera();
        }

    }
}
