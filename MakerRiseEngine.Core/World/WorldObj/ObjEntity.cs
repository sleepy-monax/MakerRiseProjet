using Microsoft.Xna.Framework;
using Maker.RiseEngine.Core.Storage.NamedBinaryTag.Tags;
using Maker.RiseEngine.Core.World.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Maker.RiseEngine.Core.World.WorldObj
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
        public WorldLocation Location;
        public int Action = -1;
        public int ActionProgress = 0;

        //Tags

        NbtCompound rootNbtCompound;

        //entity Moving
        public Vector2 OnTileLocation = Vector2.Zero;

        public bool IsFocus = false;

        public ObjEntity(int _ID, int _Variant)
        {
            ID = _ID;
            Variant = _Variant;

            rootNbtCompound = new NbtCompound();
        }


    }
}
