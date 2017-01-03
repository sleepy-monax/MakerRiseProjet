using Maker.RiseEngine.Core.GameObject;

namespace Maker.twiyol.Game.WorldDataStruct
{
    public static class WorldDataHelper
    {

        public static IEntity ToGameObject(this DataEntity Entity)
        {

            return GameObjectsManager.GetGameObject<IEntity>(Entity.ID);

        }

        public static ITile ToGameObject(this DataTile Tile)
        {

            return GameObjectsManager.GetGameObject<ITile>(Tile.ID);

        }

    }
}
