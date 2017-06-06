using Maker.RiseEngine.GameObjects;
using Maker.RiseEngine.Plugin;
using Maker.RiseEngine.Ressources;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Maker.RiseEngine.Rendering.SpriteSheets
{
    public class SpriteSheet : IGameObject
    {
        public string GameObjectName { get; set; }

        public Dictionary<string, Sprite> Sprites;
        public Texture2D Texture;
        public Point TileSize;

        public string PluginName
        {
            get;
            set;
        }

        public SpriteSheet(GameEngine engine, string pluginName, DataFile SpriteSheetData)
        {
            if (SpriteSheetData.DataType == "SpriteSheet")
            {
                Texture = engine.ressourceManager.GetTexture2D(pluginName, SpriteSheetData.GetDataAsString("SpriteSheet.Texture")); 
                string[] rawTileSize = SpriteSheetData.GetDataAsString("SpriteSheet.TileSize").Split('x');
                TileSize = new Point(int.Parse(rawTileSize[0]), int.Parse(rawTileSize[1]));
                string[] spritesNames = SpriteSheetData.GetDataAsString("SpriteSheet.Sprites").Split(',');

                foreach (var spriteName in spritesNames)
                {
                    string[] spriteData = SpriteSheetData.GetDataAsString($"Sprite.{spriteName}").Split(',');

                    if (spriteData[0] == "Static")
                    {
                        string[] rawSpriteSize = spriteData[1].Split('x');
                        string[] rawSpriteLocation = spriteData[2].Split('-');

                        Frame frame = new Frame(int.Parse(rawSpriteLocation[0]), int.Parse(rawSpriteLocation[1]), int.Parse(rawSpriteSize[0]), int.Parse(rawSpriteSize[1]));
                        Sprites.Add(spriteName, new Sprite(this, frame));
                    }
                    else if (spriteData[0] == "Animated")
                    {

                    }
                    else
                    {
                        Debug.WriteLog("Unknow sprite type:" + spriteData[0], LogType.Error, nameof(SpriteSheet));
                    }
                }
            }
        }

        public Sprite GetSprite(string spriteName)
        {
            return Sprites[spriteName];

        }

        public void OnGameObjectAdded()
        {
        }
    }
}
