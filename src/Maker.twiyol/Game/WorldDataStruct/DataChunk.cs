using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Maker.twiyol.Game.WorldDataStruct
{

    public enum chunkStatutList { Done, onDecoration, needDecoration }

    [Serializable]
    public class DataChunk
    {
        public Dictionary<int, DataEntity> Entities = new Dictionary<int, DataEntity>();
        public DataTile[,] Tiles = new DataTile[16, 16];


        public chunkStatutList chunkStatut = chunkStatutList.needDecoration; //cette valeur est passé à true quand le chunk a été décoré.

        public void AddEntity(DataEntity _Entity, Point Location)
        {
            int EntityID = (Location.Y * 16) + Location.X;

            if (!this.Entities.ContainsKey(EntityID))
            {
                this.Entities.Add(EntityID, _Entity);
                this.Tiles[Location.X, Location.Y].Entity = EntityID;
            }
            else
            {

                RiseEngine.Core.EngineDebug.DebugLogs.WriteLog("Illegal placing at " + EntityID, RiseEngine.Core.EngineDebug.LogType.Warning, "ObjChunk");

            }


        }
    }
}
