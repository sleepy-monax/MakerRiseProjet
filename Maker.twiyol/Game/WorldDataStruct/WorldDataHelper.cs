

using Maker.RiseEngine.Core.GameObject;
using Maker.twiyol.GameObject;

namespace Maker.twiyol.Game.WorldDataStruct
{
    public static class WorldDataHelper
    {

        public static IEntity ToGameObject(this DataEntity Entity)
        {

            return GameObjectManager.GetGameObject<IEntity>(Entity.ID);

        }

        public static ITile ToGameObject(this DataTile Tile)
        {

            return GameObjectManager.GetGameObject<ITile>(Tile.ID);

        }

    }
}
