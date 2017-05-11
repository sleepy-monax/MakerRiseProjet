using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Twiyol.GameObject
{
    public class ItemDrop
    {
        public int   ID;
        public int   Variant;
        public int   Count;
        public float DropChance;

        public ItemDrop(int id, int variant, int count, float dropChance)
        {
            ID = id;
            Variant = variant;
            Count = count;
            DropChance = dropChance;
        }
    }
}
