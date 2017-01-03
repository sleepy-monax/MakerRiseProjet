using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiseEngine.Core.GameObject
{

    public enum ItemType
    {
        ArmorHelmet,
        ArmorChestplate,
        ArmorLeggings,
        ArmorBoots,
        Crafting,
        Food,
        shields,
        gathering,
        potions,
        Tools,
        Weapon
    }

    public class Item
    {

        public ItemType Type { get; set; }
        public List<Rendering.SpriteSheets.Sprite> Variant;
        public string Name;

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
