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

        public string gameObjectName{get;set;}

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
            System.IO.StreamReader srMap = new System.IO.StreamReader("Data\\" + PluginName + "\\SpriteSheet\\" + _TileMapName + ".rise");
            string SheetMapString = srMap.ReadToEnd().ToDosLineEnd().Replace(System.Environment.NewLine, "");
            srMap.Close();

            // Create new instance of TilesColection
            SpriteColection = new Dictionary<string, TilesheetColectionItem>();

            string[] Lines = SheetMapString.Split(';');
            for (int i = 0; i < Lines.Length; i++)
            {
                //animated Sprite
                string[] str = Lines[i].Split(':');

                if (Lines[i].StartsWith("@"))
                {

                    if (str.Count() == 2)
                    {

                        string[] SubArgs = str[1].Split(',');
                        string SpriteName = str[0];
                        int SpriteCount = int.Parse(SubArgs[0]);
                        string[] Sprites = new string[SpriteCount];

                        for (int s = 0; s < SpriteCount; s++)
                        {

                            Sprites[s] = SpriteName.Remove(0, 1) + s;

                        }

                        SpriteColection.Add(SpriteName.Remove(0, 1), new TilesheetColectionItem(Sprites, (AnimationMode)int.Parse(SubArgs[2]), int.Parse(SubArgs[1])));

                    }
                    else
                    {

                        EngineDebug.DebugLogs.WriteInLogs("Syntaxe error on '" + PluginName + "." + _TileMapName + "' Ln" + (i + 1), EngineDebug.LogType.Warning, "SpriteSheetParse");

                    }

                }
                else if (Lines[i].StartsWith("//"))
                {
                    //Do Nothing 

                }
                else
                {


                    //Check for syntaxe.
                    if (str.Count() == 3)
                    {

                        SpriteColection.Add(str[0], new TilesheetColectionItem(
                            //Tile Location
                            int.Parse(str[1].Split(',')[0]),
                            int.Parse(str[1].Split(',')[1]),

                            //Tile Size
                            int.Parse(str[2].Split(',')[0]),
                            int.Parse(str[2].Split(',')[1])
                            ));

                    }
                    else if (str.Count() == 2)
                    {

                        SpriteColection.Add(str[0], new TilesheetColectionItem(
                            //Tile Location
                            int.Parse(str[1].Split(',')[0]),
                            int.Parse(str[1].Split(',')[1]),

                            //Tile Size
                            1,
                            1
                            ));

                    }
                    else
                    {

                        EngineDebug.DebugLogs.WriteInLogs("Syntaxe error on '" + PluginName + "." + _TileMapName + "' Ln" + (i + 1), EngineDebug.LogType.Warning, "SpriteSheetParse ");

                    }

                }


            }

        }

        public Sprite GetSprite(string _SpriteName)
        {
            if (SpriteColection.ContainsKey(_SpriteName))
            {
                TilesheetColectionItem sI = SpriteColection[_SpriteName];

                if (sI.Animated == true)
                {

                    TilesheetColectionItem[] Frames = new TilesheetColectionItem[sI.Frames.Count()];

                    for (int I = 0; I < sI.Frames.Length; I++)
                    {

                        Frames[I] = SpriteColection[sI.Frames[I]];

                    }

                    return new Sprite(this, sI, Frames);

                }
                else
                {
                    return new Sprite(this, sI);
                }


            }
            else
            {
                EngineDebug.DebugLogs.WriteInLogs("Missing Sprite '" + _SpriteName + "'", EngineDebug.LogType.Warning, "SpriteSheetParse");
                return null;
            }


        }

        public void OnGameObjectAdded()
        {
        }
    }
}
