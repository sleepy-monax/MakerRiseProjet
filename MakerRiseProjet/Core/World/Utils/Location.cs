using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.World.Utils
{
    public static class Location
    {

        //Converti un point en worldLocation
        public static WorldLocation ToWorldLocation(this Point Location)
        {

            WorldLocation WipLocation = new WorldLocation();

            Point ChunkXY = new Point();
            Point TileXY = new Point();

            ChunkXY.X = Location.X / 16;
            TileXY.X = Location.X % 16;
            if (TileXY.X < 0)
            {
                TileXY.X = 0;
            }


            ChunkXY.Y = Location.Y / 16;
            TileXY.Y = Location.Y % 16;
            if (TileXY.Y < 0)
            {
                TileXY.Y = 0;
            }

            WipLocation.tile = TileXY;
            WipLocation.chunk = ChunkXY;

            return WipLocation;

        }

        //Converti une worldLocation en un point
        public static Point ToPoint(this WorldLocation WorldLocation)
        {
            return new Point(WorldLocation.chunk.X * 16 + WorldLocation.tile.X, WorldLocation.chunk.Y * 16 + WorldLocation.tile.Y);
        }

        //ajoute les cooordonné d'un point a une worldLOcation
        public static WorldLocation AddPoint(this WorldLocation _WorldLocation, Point _Point)
        {

            Point pt = ToPoint(_WorldLocation);
            pt = pt + _Point;

            WorldLocation NewWorldLocation = ToWorldLocation(pt);

            return NewWorldLocation;
        }
    }



    public class WorldLocation
    {

        public WorldLocation() { }
        public WorldLocation(Point _Chunk, Point _Tile)
        {
            chunk = _Chunk;
            tile = _Tile;
        }
        public Point chunk = Point.Zero;
        public Point tile = Point.Zero;
    }
}
