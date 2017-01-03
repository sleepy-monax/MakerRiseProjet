using Maker.twiyol.Game.GameUtils;
using Microsoft.Xna.Framework;
using System;


namespace Maker.twiyol.Game.WorldDataStruct
{
    [Serializable]
    public class DataEntity
    {

        public int ID = 0;
        public int Variant = 0;

        //Stats

        public float maxHeal = 20;
        public float heal = 20;
        
        //IA
        public AI.Utils.Facing Facing = AI.Utils.Facing.Down;
        public AI.Utils.Facing[] Path;
        public WorldLocation Location;
        public int Action = -1;
        public int ActionProgress = 0;

        //entity Moving
        public float OnTileLocationX = 0;
        public float OnTileLocationY = 0;

        public void SetOnTileLocation(Vector2 v) {
            OnTileLocationX = v.X;
            OnTileLocationY = v.Y;
        }

        public Vector2 GetOnTileLocation() {
            return new Vector2(OnTileLocationX, OnTileLocationY);
        }


        public bool IsFocus = false;

        public DataEntity(int _ID, int _Variant)
        {
            ID = _ID;
            Variant = _Variant;
        }

        

    }
}
