using Maker.RiseEngine.Core.GameObjects;
using Maker.Twiyol.Game.WorldDataStruct.Tags;
using Maker.Twiyol.GameObject.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Twiyol.Inventory
{
    [Serializable]
    public class DataItem
    {

        public int ID = 0;
        public int Variante = 0;
        public int Count = 0;

        public DataItem(int id, int variante = 0, int count = 1) {

            ID = id;
            Variante = variante;
            Count = count;

        }

        public Item ToGameObject() {
            return GameObjectManager.GetGameObject<Item>(ID);
        }

    }
}
