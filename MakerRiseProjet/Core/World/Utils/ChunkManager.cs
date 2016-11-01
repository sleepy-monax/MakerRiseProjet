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
        public WorldObj.ObjChunk GetChunk(Utils.WorldLocation ChunkLocation)
        {
            return GetChunk(ChunkLocation.chunk.X, ChunkLocation.chunk.Y);
        }

        public WorldObj.ObjChunk GetChunk(Point ChunkLocation)
        {
            return GetChunk(ChunkLocation.X, ChunkLocation.Y);
        }

        public WorldObj.ObjChunk GetChunk(int x, int y)
        {
            if (W.Chunks[x, y].IsDone == false)
            {

                W.Chunks[x, y] = W.chunkDecorator.Decorated(x, y, W.Chunks[x, y]);
                return W.Chunks[x, y];

            }
            else
            {
                return W.Chunks[x, y];
            }
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

