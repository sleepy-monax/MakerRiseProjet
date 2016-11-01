using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiseEngine.Core.World.WorldObj
{
    [Serializable]
    public class ObjTile
    {
        public int ID = -1;
        public int Variant = 0;
        public int Entity = -1; //si cette valeur est = à -1 alors il n'y a pas d'entity prensente.
        public int LightLevel = 0;
        public int Region = 0;

    }
}
