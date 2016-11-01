using Microsoft.Xna.Framework;
using RiseEngine.Core.Storage.NamedBinaryTag.Tags;
using RiseEngine.Core.World.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RiseEngine.Core.World.WorldObj
{
    [Serializable]
    public class ObjEntity
    {

        public int ID = 0;
        public int Variant = 0;

        //IA
        public AI.Utils.Facing Facing = AI.Utils.Facing.Down;
        public WorldLocation Location;
        public int Action = -1;
        public int ActionProgress = 0;

        //Tags

        Storage.NamedBinaryTag.Tags.NbtCompound rootCompound;

        //entity Moving
        public Vector2 OnTileLocation = Vector2.Zero;

        public bool IsFocus = false;

        public ObjEntity(int _ID, int _Variant)
        {
            ID = _ID;
            Variant = _Variant;

            rootCompound = new Storage.NamedBinaryTag.Tags.NbtCompound();
        }

        public NbtCompound ToNbtCompound()
        {

            NbtCompound nbtObjEntity = new NbtCompound();
            nbtObjEntity.Tags.Add(new NbtInt("id", ID));
            nbtObjEntity.Tags.Add(new NbtInt("variant", Variant));
            nbtObjEntity.Tags.Add(new NbtInt("facing", (int)Facing));

            return nbtObjEntity;

        }
    }
}
