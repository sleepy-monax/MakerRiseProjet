using Maker.twiyol.Core.Game.GameUtils;
using Maker.twiyol.Game.WorldDataStruct;

namespace Maker.twiyol.Game.GameUtils
{
    public class EntityDataManager
    {

        GameScene G;

        public EntityDataManager(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        public void AddEntityData(WorldDataStruct.DataEntity _Entity, WorldLocation _WorldLocation)
        {
            WorldDataStruct.DataChunk Chunk = G.chunkManager.GetChunk(_WorldLocation.GetChunkPoint());
            int EntityID = _WorldLocation.tileX + _WorldLocation.tileY * 16;
            Chunk.Entities.Add(EntityID, _Entity);
            Chunk.Tiles[_WorldLocation.tileX, _WorldLocation.tileY].Entity = EntityID;
        }

        public void RemoveEntityData(WorldLocation _WorldLocation)
        {

            DataTile Tile = G.chunkManager.GetTile(_WorldLocation);
            if (Tile.Entity == -1)
            { // do nothing
            }
            else
            {

                DataChunk Chunk = G.chunkManager.GetChunk(_WorldLocation);
                Chunk.Entities.Remove(Tile.Entity);
                Tile.Entity = -1;

            }


        }
        public bool MoveEntity(WorldLocation _FromLocation, WorldLocation _ToLocation)
        {

            DataTile Tile = G.chunkManager.GetTile(_FromLocation);

            //on verifie si il y a une entitée
            if (Tile.Entity == -1)
            {
                return false;
            }


            //on verifie si le tile est libre
            if (!(IsEntityFree(_ToLocation)))
            {
                return false;
            }

            //et enfin on le deplace
            DataEntity EntityToMove = G.chunkManager.GetChunk(_FromLocation).Entities[Tile.Entity];

            RemoveEntityData(_FromLocation);
            AddEntityData(EntityToMove, _ToLocation);

            EntityToMove.Location = _ToLocation;
            return true;
        }

        public bool IsEntityFree(WorldLocation _WorldLocation)
        {

            DataTile ThisTile = G.chunkManager.GetTile(_WorldLocation);
            if (ThisTile.Entity == -1) return true;

            return false;
        }
    }
}
