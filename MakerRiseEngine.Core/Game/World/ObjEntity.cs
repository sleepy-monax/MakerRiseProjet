using Maker.RiseEngine.Core.Game.GameUtils;
using Microsoft.Xna.Framework;
using System;


namespace Maker.RiseEngine.Core.Game.World
{
    [Serializable]
    public class ObjEntity
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
        public Vector2 OnTileLocation = Vector2.Zero;

        public bool IsFocus = false;

        public ObjEntity(int _ID, int _Variant)
        {
            ID = _ID;
            Variant = _Variant;
        }


    }
}
