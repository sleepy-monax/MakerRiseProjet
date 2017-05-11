using Microsoft.Xna.Framework;
using System;

namespace Maker.Twiyol.Game.GameUtils
{
    public static class Location
    {

        //Converti un point en worldLocation
        public static WorldLocation ToWorldLocation(this Point currentLocation)
        {

            WorldLocation worldLocation = new WorldLocation();

            Point chunkLocation = new Point();
            Point tileLocation = new Point();

            chunkLocation.X = currentLocation.X / 16;
            tileLocation.X = currentLocation.X % 16;
            if (tileLocation.X < 0)
            {
                tileLocation.X = 0;
            }


            chunkLocation.Y = currentLocation.Y / 16;
            tileLocation.Y = currentLocation.Y % 16;
            if (tileLocation.Y < 0)
            {
                tileLocation.Y = 0;
            }

            worldLocation.SetTilePoint(tileLocation);
            worldLocation.SetChunkPoint(chunkLocation);

            return worldLocation;

        }

        //Converti une worldLocation en un point
        public static Point ToPoint(this WorldLocation WorldLocation)
        {
            return new Point(WorldLocation.chunkX * 16 + WorldLocation.tileX, WorldLocation.chunkY * 16 + WorldLocation.tileY);
        }

        //ajoute les cooordonné d'un point a une worldLocation
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
