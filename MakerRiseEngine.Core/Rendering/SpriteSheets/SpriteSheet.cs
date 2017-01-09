using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Maker.RiseEngine.Core.Rendering.SpriteSheets
{
    public class SpriteSheet : IGameObject
    {

        public Dictionary<string, TilesheetColectionItem> SpriteColection;
        public Texture2D SpriteSheetTexture2D;
        public Point SpriteSize;

        public string GameObjectName { get; set; }

        public string pluginName
        {
            get;
            set;
        }

        public SpriteSheet(string PluginName, Texture2D _SpriteSheet, string _SpriteMapName, Point _SpriteSize)
        {

            SpriteSheetTexture2D = _SpriteSheet;
            ParseSpriteMap(PluginName, _SpriteMapName);
            SpriteSize = _SpriteSize;

        }

        //This Function read map file and make a tile colection.
        public void ParseSpriteMap(string PluginName, string _TileMapName)
        {

            //Creating and read the tilemap file.
            System.IO.StreamReader spriteMapFile = new System.IO.StreamReader("Data\\" + PluginName + "\\SpriteSheet\\" + _TileMapName + ".rise");
            string SheetMapString = spriteMapFile.ReadToEnd().ToDosLineEnd().Replace(System.Environment.NewLine, "");
            spriteMapFile.Close();

            // Create new instance of TilesColection.
            SpriteColection = new Dictionary<string, TilesheetColectionItem>();

            string[] fileLines = SheetMapString.Split(';');
            for (int i = 0; i < fileLines.Length; i++)
            {
                //animated Sprite
                string[] line = fileLines[i].Split(':');

                if (fileLines[i].StartsWith("@"))
                {

                    if (line.Count() == 2)
                    {

                        string[] spriteDescription = line[1].Split(',');
                        string spriteName = line[0];
                        int SpriteCount = int.Parse(spriteDescription[0]);
                        string[] Sprites = new string[SpriteCount];

                        for (int s = 0; s < SpriteCount; s++)
                        {

                            Sprites[s] = spriteName.Remove(0, 1) + s;

                        }

                        SpriteColection.Add(spriteName.Remove(0, 1), new TilesheetColectionItem(Sprites, (AnimationMode)int.Parse(spriteDescription[2]), int.Parse(spriteDescription[1])));

                    }
                    else
                    {

                        EngineDebug.DebugLogs.WriteLog("Syntaxe error on '" + PluginName + "." + _TileMapName + "' Ln" + (i + 1), EngineDebug.LogType.Warning, "SpriteSheetParse");

                    }

                }
                else if (fileLines[i].StartsWith("//"))
                {
                    //Is comment line.
                }
                else
                {


                    //Check for syntaxe.
                    if (line.Count() == 3)
                    {

                        SpriteColection.Add(line[0], new TilesheetColectionItem(
                            //Tile Location
                            int.Parse(line[1].Split(',')[0]),
                            int.Parse(line[1].Split(',')[1]),

                            //Tile Size
                            int.Parse(line[2].Split(',')[0]),
                            int.Parse(line[2].Split(',')[1])
                            ));

                    }
                    else if (line.Count() == 2)
                    {

                        SpriteColection.Add(line[0], new TilesheetColectionItem(
                            //Tile Location
                            int.Parse(line[1].Split(',')[0]),
                            int.Parse(line[1].Split(',')[1]),

                            //Tile Size
                            1,
                            1
                            ));

                    }
                    else
                    {

                        EngineDebug.DebugLogs.WriteLog("Syntaxe error on '" + PluginName + "." + _TileMapName + "' Ln" + (i + 1), EngineDebug.LogType.Warning, "SpriteSheetParse ");

                    }

                }


            }

        }

        public Sprite GetSprite(string spriteName)
        {
            if (SpriteColection.ContainsKey(spriteName))
            {
                TilesheetColectionItem tileSheetColectionItem = SpriteColection[spriteName];

                if (tileSheetColectionItem.Animated == true)
                {

                    TilesheetColectionItem[] animationFrames = new TilesheetColectionItem[tileSheetColectionItem.Frames.Count()];

                    for (int I = 0; I < tileSheetColectionItem.Frames.Length; I++)
                    {

                        animationFrames[I] = SpriteColection[tileSheetColectionItem.Frames[I]];

                    }

                    return new Sprite(this, tileSheetColectionItem, animationFrames);

                }
                else
                {
                    return new Sprite(this, tileSheetColectionItem);
                }


            }
            else
            {
                EngineDebug.DebugLogs.WriteLog($"Missing Sprite '{spriteName}'", EngineDebug.LogType.Warning, "SpriteSheetParse");
                return null;
            }


        }

        public void OnGameObjectAdded()
        {
        }
    }
}
