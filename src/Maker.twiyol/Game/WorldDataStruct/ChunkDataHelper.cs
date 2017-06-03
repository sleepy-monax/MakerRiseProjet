using Maker.Twiyol.Game.WorldDataStruct;
using Microsoft.Xna.Framework;

namespace Maker.Twiyol.Game.GameUtils
{
    public static class ChunkDataHelper

    {

        #region GetChunk
        //Get Chunk On location
        public static DataChunk GetChunk(this DataWorld world, WorldLocation ChunkLocation)
        {
            return world.GetChunk(ChunkLocation.chunkX, ChunkLocation.chunkY);
        }

        public static DataChunk GetChunk(this DataWorld world, Point ChunkLocation)
        {
            return world.GetChunk(ChunkLocation.X, ChunkLocation.Y);
        }

        public static DataChunk GetChunk(this DataWorld world, int x, int y)
        {
            return world.chunks[x, y];
        }
        #endregion

        #region GetTile

        public static DataTile GetTile(this DataWorld world, WorldLocation _WorldLocation)
        {

            DataChunk Chunk = world.GetChunk(_WorldLocation.GetChunkPoint());
            return Chunk.Tiles[_WorldLocation.tileX, _WorldLocation.tileY];

        }

        public static DataTile GetTile(this DataWorld world, Point _Location)
        {

            return world.GetTile(_Location.ToWorldLocation());

        }
        #endregion

        #region GetEntity

        public static DataEntity GetEntity(this DataWorld world, WorldLocation worldLocation)
        {

            DataChunk chunk = world.GetChunk(worldLocation);
            return chunk.Entities[world.GetTile(worldLocation).Entity];

        }

        public static DataEntity GetEntity(this DataWorld world, Point location)
        {

            return world.GetEntity(location.ToWorldLocation());

        }

        #endregion
    }

}

