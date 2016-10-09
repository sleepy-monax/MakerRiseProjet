using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiseEngine.Core.Rendering.SpriteSheets;

namespace RiseEngine.Core.GameObject.Items
{
    public class Item : IItem
    {
        public ItemType Type { get; set; }

        public List<Sprite> Variant { get; set; }

        public string Name{get;set; }

        public Item(string _Name, ItemType _Type, string[] _SpriteVariant, string _SpriteSheet)
        {

            Name = _Name;
            Type = _Type;

            Variant = new List<Rendering.SpriteSheets.Sprite>();
            foreach (string str in _SpriteVariant)
            {

                Variant.Add(GameObjectsManager.SpriteSheets[_SpriteSheet].GetSprite(str));

            }


        }
    }
}
