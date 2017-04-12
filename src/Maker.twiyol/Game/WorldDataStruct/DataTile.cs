using System;

namespace Maker.twiyol.Game.WorldDataStruct
{
    [Serializable]
    public class DataTile
    {
        public int ID = -1;
        public int Variant = 0;
        public int Entity = -1; // If this value is = -1, there are no etity on the tile.
        public int Region = 0;

        public Tags.TagManger Tags = new Tags.TagManger();

    }
}
