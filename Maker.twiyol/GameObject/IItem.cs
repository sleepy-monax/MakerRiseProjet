using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameObjects;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
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
        Sprite ItemSprite { get; set; }

    }
}
