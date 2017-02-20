

using Maker.RiseEngine.Core.GameComponent;
using Maker.twiyol.GameObject;

namespace Maker.twiyol.Game.WorldDataStruct
{
    public static class WorldDataHelper
    {

        public static IEntity ToGameObject(this DataEntity Entity)
        {

            return GameComponentManager.GetGameObject<IEntity>(Entity.ID);

        }

        public static ITile ToGameObject(this DataTile Tile)
        {

            return GameComponentManager.GetGameObject<ITile>(Tile.ID);

        }

    }
}
