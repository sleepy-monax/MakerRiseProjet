using Maker.RiseEngine.Core.GameObjects;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using System.Collections.Generic;

namespace Maker.Twiyol.GameObject.Items
{
    public class Item : IItem
    {
        public string GameObjectName { get; set; }
        public string PluginName { get; set; }

        public ItemType Type { get; set; }
        public Sprite ItemSprite { get; set; }

        public int MaxStackSize { get; set; } = 64;

        public Item(ItemType type, string spriteName, int spriteSheetID, int maxStackSize = 64)
        {
            Type = type;
            ItemSprite =  GameComponentManager.GetGameObject<SpriteSheet>(spriteSheetID).GetSprite(spriteName);
            MaxStackSize = maxStackSize;
        }

        public void OnGameObjectAdded()
        {

        }
    }
}
