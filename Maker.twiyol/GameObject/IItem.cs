using System.Collections.Generic;

namespace Maker.twiyol.GameObject
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

    public interface IItem: IGameObject
    {

        
        ItemType Type { get; set; }
        List<Rendering.SpriteSheets.Sprite> Variant { get; set; }

    }
}
