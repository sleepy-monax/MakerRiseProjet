using Maker.RiseEngine.Core.Game.World;
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
        public ObjChunk GetChunk(GameUtils.WorldLocation ChunkLocation)
        {
            return GetChunk(ChunkLocation.chunk.X, ChunkLocation.chunk.Y);
        }

        public ObjChunk GetChunk(Point ChunkLocation)
        {
            return GetChunk(ChunkLocation.X, ChunkLocation.Y);
        }

        public bool PrepareChunk(int x, int y) {

            ObjChunk chunk = G.world.chunks[x, y];

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

        public ObjChunk GetChunk(int x, int y)
        {
                return G.world.chunks[x, y];
        }
        #endregion

        #region GetTile

        public ObjTile GetTile(WorldLocation _WorldLocation)
        {

            World.ObjChunk Chunk = GetChunk(_WorldLocation.chunk);
            return Chunk.Tiles[_WorldLocation.tile.X, _WorldLocation.tile.Y];

        }

        public ObjTile GetTile(Point _Location)
        {

            return GetTile(_Location.ToWorldLocation());

        }
        #endregion

        #region GetEntity

        public ObjEntity GetEntity(WorldLocation _WorldLocation)
        {

            ObjChunk chk = GetChunk(_WorldLocation);
            return chk.Entities[GetTile(_WorldLocation).Entity];

        }

        public ObjEntity GetEntity(Point _Location)
        {

            return GetEntity(_Location.ToWorldLocation());

        }

        #endregion
    }

}

