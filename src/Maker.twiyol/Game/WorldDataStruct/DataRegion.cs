using Microsoft.Xna.Framework;
using System;

namespace Maker.Twiyol.Game.WorldDataStruct
{
    [Serializable]
    public class DataRegion
    {

        public string Name;

        public int BiomeID;
        //public Color Color = Color.White;
        public GameUtils.WorldLocation Origine;
        
    }
}
