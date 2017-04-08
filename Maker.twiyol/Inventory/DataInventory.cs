using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.twiyol.Inventory
{
    [Serializable]
    public class DataInventory
    {

        public DataItem[] Slots;

        public DataInventory(int slotCount) {
            Slots = new DataItem[slotCount];
        }

        public void AddItem(DataItem item) {
            foreach (DataItem i in Slots)
            {
                if (i.ID == item.ID && i.Variante == item.Variante) {
                    i.Count += item.Count;
                }
            }
        }

    }
}
