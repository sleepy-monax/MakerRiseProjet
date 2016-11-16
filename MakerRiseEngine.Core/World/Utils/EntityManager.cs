using Maker.RiseEngine.Core.World.WorldObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.World.Utils
{
    public class EntityManager
    {

        WorldScene W;

        public EntityManager(WorldScene _WorldScene)
        {
            W = _WorldScene;
        }

        public void AddEntity(WorldObj.ObjEntity _Entity, WorldLocation _WorldLocation)
        {
            WorldObj.ObjChunk Chunk = W.chunkManager.GetChunk(_WorldLocation.chunk);
            int EntityID = _WorldLocation.tile.X + _WorldLocation.tile.Y * 16;
            Chunk.Entities.Add(EntityID, _Entity);
            Chunk.Tiles[_WorldLocation.tile.X, _WorldLocation.tile.Y].Entity = EntityID;
        }

        public void RemoveEntity(WorldLocation _WorldLocation)
        {

            ObjTile Tile = W.chunkManager.GetTile(_WorldLocation);
            if (Tile.Entity == -1)
            { // do nothing
            }
            else
            {

                ObjChunk Chunk = W.chunkManager.GetChunk(_WorldLocation);
                Chunk.Entities.Remove(Tile.Entity);
                Tile.Entity = -1;

            }


        }
        public bool MoveEntity(WorldLocation _FromLocation, WorldLocation _ToLocation)
        {

            ObjTile Tile = W.chunkManager.GetTile(_FromLocation);

            //on verifie si il y a une entitée
            if (Tile.Entity == -1)
            {
                return false;
            }


            //on verifie si le tile est libre
            if (!(TileIsFree(_ToLocation)))
            {
                return false;
            }

            //et enfin on le deplace
            ObjEntity EntityToMove = W.chunkManager.GetChunk(_FromLocation).Entities[Tile.Entity];

            RemoveEntity(_FromLocation);
            AddEntity(EntityToMove, _ToLocation);

            EntityToMove.Location = _ToLocation;
            return true;
        }

        public bool TileIsFree(WorldLocation _WorldLocation)
        {

            ObjTile ThisTile = W.chunkManager.GetTile(_WorldLocation);
            if (ThisTile.Entity == -1) return true;

            return false;
        }
    }
}
