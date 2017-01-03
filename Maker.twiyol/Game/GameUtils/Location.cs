using Microsoft.Xna.Framework;
using System;

namespace Maker.twiyol.Core.Game.GameUtils
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

            WipLocation.SetTilePoint(TileXY);
            WipLocation.SetChunkPoint(ChunkXY);

            return WipLocation;

        }

        //Converti une worldLocation en un point
        public static Point ToPoint(this WorldLocation WorldLocation)
        {
            return new Point(WorldLocation.chunkX * 16 + WorldLocation.tileX, WorldLocation.chunkY * 16 + WorldLocation.tileY);
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


    [Serializable]
    public class WorldLocation
    {

        public WorldLocation() { }
        public WorldLocation(Point chunk, Point tile)
        {
            SetChunkPoint(chunk);
            SetTilePoint(tile);
        }

        public Point GetChunkPoint() {
            return new Point(chunkX, chunkY);
        }

        public Point GetTilePoint() {
            return new Point(tileX, tileY);
        }

        public void SetChunkPoint(Point p)
        {

            chunkX = p.X;
            chunkY = p.Y;

        }

        public void SetTilePoint(Point p) {

            tileX = p.X;
            tileY = p.Y;

        }

        public int chunkX = 0;
        public int chunkY = 0;

        public int tileX = 0;
        public int tileY = 0;
    }
}
