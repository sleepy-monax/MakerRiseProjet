using Microsoft.Xna.Framework;
using System;

namespace Maker.RiseEngine.Core.Game.World
{
    [Serializable]
    public class ObjRegion
    {

        public string Name;

        public int BiomeID;
        public Color Color = Color.White;
        public GameUtils.WorldLocation Origine;
        
    }
}
