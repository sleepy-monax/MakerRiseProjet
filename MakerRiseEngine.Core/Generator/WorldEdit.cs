using System;

namespace Maker.RiseEngine.Core.Generator
{
    public static class WorldEdit
    {

        public static void SetTile(this int[,] regionGrid, int x, int y, int value)
        {

            if (x < 0) return;
            if (y < 0) return;

            if (x > regionGrid.GetLength(0) - 1) return;
            if (y > regionGrid.GetLength(1) - 1) return;

            if (regionGrid[x, y] == 0)
            {
                regionGrid[x, y] = value;
            }
        }

        public static void plotLineWidth(this int[,] regionGrid, int x0, int y0, int x1, int y1, float wd, int value)
        {
            
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx - dy, e2, x2, y2;                          /* error value e_xy */
            float ed = dx + dy == 0 ? 1 : (float)Math.Sqrt((float)dx * dx + (float)dy * dy);

            for (wd = (wd + 1) / 2; ;)
            {                                   /* pixel loop */
                regionGrid.SetTile(x0, y0, value);
                e2 = err; x2 = x0;
                if (2 * e2 >= -dx)
                {                                           /* x step */
                    for (e2 += dy, y2 = y0; e2 < ed * wd && (y1 != y2 || dx > dy); e2 += dx)
                        regionGrid.SetTile(x0, y2 += sy, value);
                    if (x0 == x1) break;
                    e2 = err; err -= dy; x0 += sx;
                }
                if (2 * e2 <= dy)
                {                                            /* y step */
                    for (e2 = dx - e2; e2 < ed * wd && (x1 != x2 || dx < dy); e2 += dy)
                        regionGrid.SetTile(x2 += sx, y0, value);
                    if (y0 == y1) break;
                    err += dx; y0 += sy;
                }
            }
        }


    }
}
