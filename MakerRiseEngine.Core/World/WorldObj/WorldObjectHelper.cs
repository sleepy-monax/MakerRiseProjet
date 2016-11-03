using RiseEngine.Core.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.World.WorldObj
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
