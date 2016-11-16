using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Inventory
{
    public class ObjItem
    {

        public int ID;
        public int Variant;
        public int Count;

        public ObjItem(int _ID, int _Variant, int _Count) {
            ID = _ID;
            Variant = _Variant;
            Count = _Count;
        }

    }
}
