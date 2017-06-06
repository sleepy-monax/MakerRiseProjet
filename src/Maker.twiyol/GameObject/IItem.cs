using Maker.RiseEngine;
using Maker.RiseEngine.GameObjects;
using Maker.RiseEngine.Rendering.SpriteSheets;
using System.Collections.Generic;

namespace Maker.Twiyol.GameObject
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
        Sprite ItemSprite { get; set; }

    }
}
