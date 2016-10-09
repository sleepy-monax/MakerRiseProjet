using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RiseEngine.Core.Rendering.SpriteSheets;

namespace RiseEngine.Core
{


    public class GameObjectsManager
    {

        static bool IsLoaded = false;



        //Game Object
        public static Dictionary<string, SpriteSheet> SpriteSheets = new Dictionary<string, SpriteSheet>();



        public static List<string> GetGameObjList() {

            List<string> GameobjIDlist = new List<string>();

            foreach (KeyValuePair<string, int> i in TileKeys) {

                GameobjIDlist.Add("Tiles." + i.Key);

            }

            foreach (KeyValuePair<string, int> i in EntityKey)
            {
                
                GameobjIDlist.Add("Entities." + i.Key);

            }

            foreach (KeyValuePair<string, int> i in ItemKeys)
            {

                GameobjIDlist.Add("Items." + i.Key);

            }

            foreach (KeyValuePair<string, int> i in BiomeKey)
            {

                GameobjIDlist.Add("Biomes." + i.Key);

            }

            return GameobjIDlist;
        }
        
        #region Item

        public static Dictionary<string, int> ItemKeys = new Dictionary<string, int>();
        public static Dictionary<int, Core.GameObject.IItem> Items = new Dictionary<int, Core.GameObject.IItem>();

        public static void AddItem(Plugin.IPlugin _Plugin, string _Name, Core.GameObject.IItem _Item)
        {
            Debug.Logs.Write("[Plugin." + _Plugin.Name + "] <Item>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            ItemKeys.Add(_Plugin.Name + "." + _Name, Items.Count());
            Items.Add(Items.Count, _Item);
            
        }

        #endregion


        #region Biome

        public static Dictionary<string, int> BiomeKey = new Dictionary<string, int>();
        public static Dictionary<int, Core.GameObject.Biome> Biomes = new Dictionary<int, Core.GameObject.Biome>();

        public static void AddBiome(Plugin.IPlugin _Plugin, string _Name, Core.GameObject.Biome _Biome)
        {
            Debug.Logs.Write("[Plugin." + _Plugin.Name + "] <Biome>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            BiomeKey.Add(_Plugin.Name + "." + _Name, Biomes.Count());
            Biomes.Add(Biomes.Count, _Biome);

        }

        #endregion

        #region Entities

        public static Dictionary<string, int> EntityKey = new Dictionary<string, int>();
        public static Dictionary<int, Core.GameObject.IEntity> Entities = new Dictionary<int, GameObject.IEntity>();

        public static void AddEntity(Plugin.IPlugin _Plugin, string _Name, Core.GameObject.IEntity _Entity)
        {
            Debug.Logs.Write("[Plugin." + _Plugin.Name + "] <Entity>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            EntityKey.Add(_Plugin.Name + "." + _Name, Entities.Count());
            Entities.Add(Entities.Count, _Entity);

        }

        #endregion

        #region Tiles

        public static Dictionary<string, int> TileKeys = new Dictionary<string, int>();
        public static Dictionary<int, Core.GameObject.ITile> Tiles = new Dictionary<int, Core.GameObject.ITile>();

        public static void AddTile(Plugin.IPlugin _Plugin, string _Name, Core.GameObject.ITile _Tile)
        {
            Debug.Logs.Write("[Plugin." + _Plugin.Name + "] <Tile>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            TileKeys.Add(_Plugin.Name + "." + _Name, Tiles.Count());
            Tiles.Add(Tiles.Count, _Tile);

        }

        #endregion

        #region  security

        public static bool IsFullLoaded()
        {

            if (Tiles.Count >= 1 && Entities.Count >= 1 && Biomes.Count >= 1 && Items.Count >= 1)
                return true;
            return false;
        }

        #endregion

        public static void Reload() {

            Debug.Logs.Write("[Plugin] Reloading...", Debug.LogType.Info);

            IsLoaded = false;

            SpriteSheets.Clear();

            Items.Clear();
            ItemKeys.Clear();

            Biomes.Clear();
            BiomeKey.Clear();

            Entities.Clear();
            EntityKey.Clear();

            Tiles.Clear();
            TileKeys.Clear();

             
        }

        #region Plugin

        public static Dictionary<string, Plugin.IPlugin> Plugins = new Dictionary<string, Plugin.IPlugin>();
        public static void InitializePlugin()
        {
            if (IsLoaded == false)
            {

                //LoadPlugin
                foreach (string Dir in Directory.GetDirectories("Data"))
                {

                    Plugin.Builder.BuildPlugin(Dir);
                    

                    if (File.Exists(Dir + "\\Main.cs"))
                    {

                        string References = "";
                        foreach (string f in System.IO.Directory.GetFiles(Dir + "\\Assemblies\\"))
                        {
                            References += f + ";";

                        }

                        if (Plugin.Builder.Build(Dir + "\\Main.cs", Dir + "\\Plugin.dll"))
                        {

                            ICollection<Plugin.IPlugin> PluginCollection = Plugin.PluginLoader.LoadPluguins(Dir + "\\Plugin.dll");

                            if (PluginCollection.Count == 0)
                            {

                                Debug.Logs.Write("[Plugin] " + Dir.Split('\\')[1] + " is not a plugin !", Debug.LogType.Warning);

                            }
                            else
                            {

                                foreach (Plugin.IPlugin i in PluginCollection)
                                {
                                    //Load All plugin
                                    Plugins.Add(i.Name, i);

                                    Debug.Logs.Write("[Plugin." + i.Name + "] Initializing...", Debug.LogType.Info);
                                    Plugins[i.Name].Initialize();

                                }
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }

            IsLoaded = true;
        }
        #endregion
    }
}
