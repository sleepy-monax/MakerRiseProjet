using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiseEngine.Core.World.WorldObj
{
    [Serializable]
    public class ObjChunk
    {
        public Dictionary<int, WorldObj.ObjEntity> Entities = new Dictionary<int, WorldObj.ObjEntity>();
        public WorldObj.ObjTile[,] Tiles = new ObjTile[16, 16];

        
        public bool IsDone = false; //cette valeur est passé à true quand le chunk a été décoré.

        public void AddEntity(WorldObj.ObjEntity _Entity, Point Location)
        {

                int EntityID = (Location.Y * 16) + Location.X;
                this.Entities.Add(EntityID, _Entity);
                this.Tiles[Location.X, Location.Y].Entity = EntityID;

            
        }
    }
}
