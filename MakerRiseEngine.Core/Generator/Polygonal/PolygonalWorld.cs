using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Generator.Polygonal
{
    public class PolygonalWorld
    {

        public Node[,] NodesGrid;
        public Cell[,] CellsGrid;

        public PolygonalWorld(int Size) {

            NodesGrid = new Node[Size + 1, Size + 1];
            CellsGrid = new Cell[Size, Size];

        }

    }
}
