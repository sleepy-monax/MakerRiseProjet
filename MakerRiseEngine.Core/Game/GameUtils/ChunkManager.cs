using Maker.RiseEngine.Core.Game.WorldDataStruct;
using Microsoft.Xna.Framework;

namespace Maker.RiseEngine.Core.Game.GameUtils
{
    public class ChunkManager
    {
        GameScene G;

        public ChunkManager(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        #region GetChunk
        //Get Chunk On location
        public DataChunk GetChunk(GameUtils.WorldLocation ChunkLocation)
        {
            return GetChunk(ChunkLocation.chunkX, ChunkLocation.chunkY);
        }

        public DataChunk GetChunk(Point ChunkLocation)
        {
            return GetChunk(ChunkLocation.X, ChunkLocation.Y);
        }

        public bool PrepareChunk(int x, int y) {

            DataChunk chunk = G.world.chunks[x, y];

            switch (chunk.chunkStatut)
            {
                case chunkStatutList.Done:
                    return true;
                case chunkStatutList.onDecoration:
                    return false;
                case chunkStatutList.needDecoration:
                    G.chunkDecorator.Decorated(x, y, G.world.chunks[x, y]);
                    return false;
                default:
                    break;
            }

            return false;
        }

        public DataChunk GetChunk(int x, int y)
        {
                return G.world.chunks[x, y];
        }
        #endregion

        #region GetTile

        public DataTile GetTile(WorldLocation _WorldLocation)
        {

            DataChunk Chunk = GetChunk(_WorldLocation.GetChunkPoint());
            return Chunk.Tiles[_WorldLocation.tileX, _WorldLocation.tileY];

        }

        public DataTile GetTile(Point _Location)
        {

            return GetTile(_Location.ToWorldLocation());

        }
        #endregion

        #region GetEntity

        public DataEntity GetEntity(WorldLocation _WorldLocation)
        {

            DataChunk chk = GetChunk(_WorldLocation);
            return chk.Entities[GetTile(_WorldLocation).Entity];

        }

        public DataEntity GetEntity(Point _Location)
        {

            return GetEntity(_Location.ToWorldLocation());

        }

        #endregion
    }

}

