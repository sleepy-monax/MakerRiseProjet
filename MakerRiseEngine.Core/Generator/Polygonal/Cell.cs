using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Generator.Polygonal
{
    public class Cell
    {

        public Vector2 location;

        public bool water;  // lake or ocean
        public bool ocean;  // ocean
        public bool coast;  // land polygon touching an ocean
        public bool border;  // at the edge of the map
        public int biome;  // biome type (see article)
        public int elevation;  // 0.0-1.0
        public int moisture;

    }
}
