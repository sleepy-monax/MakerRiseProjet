using Maker.RiseEngine.Core.GameObject;

namespace Maker.RiseEngine.Core.Game.World
{
    public static class WorldObjectHelper
    {

        public static IEntity ToGameObject(this ObjEntity Entity)
        {

            return GameObjectsManager.GetGameObject<IEntity>(Entity.ID);

        }

        public static ITile ToGameObject(this ObjTile Tile)
        {

            return GameObjectsManager.GetGameObject<ITile>(Tile.ID);

        }

    }
}
