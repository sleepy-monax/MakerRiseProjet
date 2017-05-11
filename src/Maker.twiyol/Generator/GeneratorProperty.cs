using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Twiyol.Generator
{
    public class GeneratorProperty
    {
        public string WorldName;
        public int Seed;
        public int MaxRegionCount;
        public int RegionExpention;
        public int WorldSize;

        public GeneratorProperty(string worldName, int seed = 0, int worldSize=24, int regioncount = 30, int regionExpention = 300) {

            WorldName = worldName;
            WorldSize = worldSize;
            Seed = seed;
            MaxRegionCount = regioncount;
            RegionExpention = regionExpention;
            WorldSize = worldSize;

        }
    } 
}
