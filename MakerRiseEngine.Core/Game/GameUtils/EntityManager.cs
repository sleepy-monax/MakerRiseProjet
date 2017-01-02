using Maker.RiseEngine.Core.Game.WorldDataStruct;

namespace Maker.RiseEngine.Core.Game.GameUtils
{
    public class EntityManager
    {

        GameScene G;

        public EntityManager(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        public void AddEntity(WorldDataStruct.DataEntity _Entity, WorldLocation _WorldLocation)
        {
            WorldDataStruct.DataChunk Chunk = G.chunkManager.GetChunk(_WorldLocation.GetChunkPoint());
            int EntityID = _WorldLocation.tileX + _WorldLocation.tileY * 16;
            Chunk.Entities.Add(EntityID, _Entity);
            Chunk.Tiles[_WorldLocation.tileX, _WorldLocation.tileY].Entity = EntityID;
        }

        public void RemoveEntity(WorldLocation _WorldLocation)
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
            if (!(TileIsFree(_ToLocation)))
            {
                return false;
            }

            //et enfin on le deplace
            DataEntity EntityToMove = G.chunkManager.GetChunk(_FromLocation).Entities[Tile.Entity];

            RemoveEntity(_FromLocation);
            AddEntity(EntityToMove, _ToLocation);

            EntityToMove.Location = _ToLocation;
            return true;
        }

        public bool TileIsFree(WorldLocation _WorldLocation)
        {

            DataTile ThisTile = G.chunkManager.GetTile(_WorldLocation);
            if (ThisTile.Entity == -1) return true;

            return false;
        }
    }
}
