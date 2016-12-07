using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Generator.Polygonal
{
    public class Node
    {
        public float x = 0, y = 0;
        public float elevation;  // 0.0-1.0
        public int moisture;
        int riverSize = 0;
    }
}
