using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiseEngine.Core.World;
using Microsoft.Xna.Framework;
using RiseEngine.Core.World.Utils;

namespace RiseEngine.Core.Generator.Features
{
    public class Road : BaseFeature
    {

        public int RoadMaxLenght = 128;

        public override void Apply(int[,] _Grid, System.Drawing.Bitmap _Bitmap, WorldScene _World)
        {

            foreach (KeyValuePair<int, World.Obj.ObjRegion> i in _World.Region)
            {

                foreach (KeyValuePair<int, World.Obj.ObjRegion> si in _World.Region)
                {

                    World.Obj.ObjRegion r1 = i.Value;
                    World.Obj.ObjRegion r2 = si.Value;


                    

                        if (r1.Origine.ToPoint().X < r2.Origine.ToPoint().X)
                        {
                        if (Math.Abs(GameMath.Utils.distance(r1.Origine.ToPoint(), r2.Origine.ToPoint())) < RoadMaxLenght)
                            drawRoad(r1.Origine.ToPoint(), r2.Origine.ToPoint(), _World, _Grid, _Bitmap);


                        }
                        else
                        {
                        if (Math.Abs(GameMath.Utils.distance(r2.Origine.ToPoint(), r1.Origine.ToPoint())) < RoadMaxLenght)
                            drawRoad(r2.Origine.ToPoint(), r1.Origine.ToPoint(), _World, _Grid, _Bitmap);

                        }
                    




                }

            }

        }

        public void drawRoad(Point p1, Point p2, WorldScene _World, int[,] _Grid, System.Drawing.Bitmap _Bitmap)
        {

            int dx = p1.X - p2.X;
            int dy = p1.Y - p2.Y;

            if (dx == 0) dx = 1;

            for (int x = p1.X; x <= p2.X; x++)
            {

                int y = (p1.Y + dy * (x - p1.X) / dx) + (int)(_World.Noise.Noise(x * 0.1, 64,64) / 0.1);

                Point pt = new Point(x, y);
                WorldLocation wl = pt.ToWorldLocation();
                try
                {
                    _World.Chunks[wl.chunk.X, wl.chunk.Y].Tiles[wl.tile.X, wl.tile.Y].ID = 2;
                    _Bitmap.SetPixel(x, y, System.Drawing.Color.Aqua);
                }
                catch (Exception)
                {


                }
                

            }

        }

    }
}
