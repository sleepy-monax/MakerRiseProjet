using RiseEngine.Core.World.WorldObj;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.World.Utils
{
    public class ChunkManager
    {
        WorldScene W;

        public ChunkManager(WorldScene _WorldScene)
        {
            W = _WorldScene;
        }

        #region GetChunk
        //Get Chunk On location
        public ObjChunk GetChunk(Utils.WorldLocation ChunkLocation)
        {
            return GetChunk(ChunkLocation.chunk.X, ChunkLocation.chunk.Y);
        }

        public ObjChunk GetChunk(Point ChunkLocation)
        {
            return GetChunk(ChunkLocation.X, ChunkLocation.Y);
        }

        public bool PrepareChunk(int x, int y) {

            ObjChunk chunk = W.Chunks[x, y];

            switch (chunk.chunkStatut)
            {
                case chunkStatutList.Done:
                    return true;
                    break;
                case chunkStatutList.onDecoration:
                    return false;
                    break;
                case chunkStatutList.needDecoration:
                    W.chunkDecorator.Decorated(x, y, W.Chunks[x, y]);
                    return false;
                    break;
                default:
                    break;
            }

            return false;

        }

        public ObjChunk GetChunk(int x, int y)
        {

                return W.Chunks[x, y];

        }
        #endregion

        #region GetTile

        public ObjTile GetTile(WorldLocation _WorldLocation)
        {

            WorldObj.ObjChunk Chunk = GetChunk(_WorldLocation.chunk);
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

