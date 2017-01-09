using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using System.Collections.Generic;

namespace Maker.twiyol.GameObject.Items
{
    public class Item : IItem
    {
        public string GameObjectName { get; set; }
        public string pluginName { get; set; }

        public ItemType Type { get; set; }
        public List<Sprite> Variant { get; set; }

        public Item(ItemType _Type, string[] _SpriteVariant, string _SpriteSheet)
        {
            Type = _Type;

            Variant = new List<Sprite>();
            foreach (string str in _SpriteVariant)
            {

                Variant.Add(GameObjectManager.GetGameObject<SpriteSheet>(_SpriteSheet.Split('.')[0], _SpriteSheet.Split('.')[1]).GetSprite(str));

            }


        }

        public void OnGameObjectAdded()
        {

        }
    }
}
