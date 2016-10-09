using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.GameObject
{
    public class Recipe
    {

        Inventory.ObjItem Result;
        Inventory.ObjItem[] Needs;

        public Recipe(Inventory.ObjItem _Result, Inventory.ObjItem[] _Needs)
        {
            Result = _Result;
            Needs = _Needs;
        }
    }
}
