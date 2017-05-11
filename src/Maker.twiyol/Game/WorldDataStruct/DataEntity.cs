using Maker.Twiyol.Game.GameUtils;
using Microsoft.Xna.Framework;
using System;


namespace Maker.Twiyol.Game.WorldDataStruct
{
    [Serializable]
    public class DataEntity
    {

        public int ID = 0;
        public int Variant = 0;

        public float OnTileOffsetX = 0;
        public float OnTileOffsetY = 0;
        public Tags.TagManger Tags = new Tags.TagManger();
        public WorldLocation Location;

        public bool IsCameraFocus = false;

        public DataEntity(int _ID, int _Variant)
        {
            ID = _ID;
            Variant = _Variant;
        }

        public void SetOnTileOffset(Vector2 v) {
            OnTileOffsetX = v.X;
            OnTileOffsetY = v.Y;
        }

        public Vector2 GetOnTileOffset() {
            return new Vector2(OnTileOffsetX, OnTileOffsetY);
        }
    }
}
