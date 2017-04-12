using Maker.twiyol.Game.WorldDataStruct;

namespace Maker.twiyol.Game.GameUtils
{
    public static class EntityDataManager
    {

        public static void AddEntityData(this DataWorld world, DataEntity _Entity, WorldLocation _WorldLocation)
        {
            DataChunk Chunk = world.GetChunk(_WorldLocation.GetChunkPoint());
            int EntityID = _WorldLocation.tileX + _WorldLocation.tileY * 16;
            Chunk.Entities.Add(EntityID, _Entity);
            Chunk.Tiles[_WorldLocation.tileX, _WorldLocation.tileY].Entity = EntityID;
        }

        public static void RemoveEntityData(this DataWorld world, WorldLocation _WorldLocation)
        {

            DataTile Tile = world.GetTile(_WorldLocation);
            if (Tile.Entity == -1)
            { // do nothing
            }
            else
            {

                DataChunk Chunk = world.GetChunk(_WorldLocation);
                Chunk.Entities.Remove(Tile.Entity);
                Tile.Entity = -1;

            }


        }
        public static bool MoveEntity(this DataWorld world, WorldLocation _FromLocation, WorldLocation _ToLocation)
        {

            DataTile Tile = world.GetTile(_FromLocation);

            //on verifie si il y a une entitée
            if (Tile.Entity == -1)
            {
                return false;
            }


            //on verifie si le tile est libre
            if (!(world.IsEntityFree(_ToLocation)))
            {
                return false;
            }

            //et enfin on le deplace
            DataEntity EntityToMove = world.GetChunk(_FromLocation).Entities[Tile.Entity];

            world.RemoveEntityData(_FromLocation);
            world.AddEntityData(EntityToMove, _ToLocation);

            EntityToMove.Location = _ToLocation;
            return true;
        }

        public static bool IsEntityFree(this DataWorld world, WorldLocation _WorldLocation)
        {

            DataTile ThisTile = world.GetTile(_WorldLocation);
            if (ThisTile.Entity == -1) return true;

            return false;
        }
    }
}
