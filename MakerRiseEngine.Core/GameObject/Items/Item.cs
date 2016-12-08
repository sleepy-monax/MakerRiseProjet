using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.GameObject.Items
{
    public class Item : IItem
    {
        public string gameObjectName { get; set; }
        public string pluginName { get; set; }

        public ItemType Type { get; set; }
        public List<Sprite> Variant { get; set; }

        public Item(ItemType _Type, string[] _SpriteVariant, string _SpriteSheet)
        {
            Type = _Type;

            Variant = new List<Rendering.SpriteSheets.Sprite>();
            foreach (string str in _SpriteVariant)
            {

                Variant.Add(GameObjectsManager.GetGameObject<Rendering.SpriteSheets.SpriteSheet>(_SpriteSheet.Split('.')[0], _SpriteSheet.Split('.')[1]).GetSprite(str));

            }


        }

        public void OnGameObjectAdded()
        {

        }
    }
}
