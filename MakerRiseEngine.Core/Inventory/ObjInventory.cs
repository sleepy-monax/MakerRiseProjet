using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Inventory
{
    public class ObjInventory
    {

        public string Name;
        public ObjItem[] Slots;

        public ObjInventory(string _Name, int _SlotCount) {

            Name = _Name;
            Slots = new ObjItem[_SlotCount];
            for (int n = 0; n < _SlotCount; n++) {
                Slots[n] = new ObjItem(-1, 0, 0);
            }

        }

    }
}
