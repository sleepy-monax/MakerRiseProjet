using System.Collections.Generic;

namespace Maker.RiseEngine.Core.GameObject
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
