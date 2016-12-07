using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Generator.Polygonal
{
    public class PolygonalWorldGenerator
    {

        #region Property
        #endregion

        public PolygonalWorld CreateWorld(int Size, int Seed) {

            // Setting up.
            Random rnd = new Random(Seed);
            PolygonalWorld polyWorld = new PolygonalWorld(Size);
            GameMath.Noise.PerlinNoise noise = new GameMath.Noise.PerlinNoise(Seed);

            // Placing Nodes.
            for (int x = 0; x < Size + 1; x++)
            {
                for (int y = 0; y < Size + 1; y++)
                {
                    Node n = new Node();
                    n.x = x + (float)rnd.NextDouble()/2;
                    n.y = y + (float)rnd.NextDouble() / 2;
                    n.elevation = (float)(Math.Abs(noise.Noise(n.x * 0.01, n.y * 0.01, 0)) * Math.Sin((x / Size) * 180) * Math.Sin((y / Size) * 180));

                    polyWorld.NodesGrid[x, y] = n;
                }
            }

            // Making edge

            return polyWorld;
        }

    }
}
