using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using RiseEngine.Core.Rendering.SpriteSheets;
using RiseEngine.Core.Plugin;

namespace RiseEngine.Core
{


    public static class GameObjectsManager
    {

        static bool IsLoaded = false;

        //Game Object
        public static Dictionary<string, int> SpriteSheetKeys = new Dictionary<string, int>();
        public static Dictionary<int, SpriteSheet> SpriteSheets = new Dictionary<int, SpriteSheet>();

        public static void AddSpriteSheet(this IPlugin _Plugin, string _Name, SpriteSheet _SpriteSheet)
        {
            Debug.DebugLogs.WriteInLogs("[Plugin." + _Plugin.Name + "] <SpriteSheet>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            SpriteSheetKeys.Add(_Plugin.Name + "." + _Name, SpriteSheets.Count());
            SpriteSheets.Add(SpriteSheets.Count, _SpriteSheet);
        }
 

        public static Dictionary<string, int> ActionKeys = new Dictionary<string, int>();
        public static Dictionary<int, Core.AI.IAction> Actions = new Dictionary<int, Core.AI.IAction>();

        public static void AddAction(this Plugin.IPlugin _Plugin, string _Name, Core.AI.IAction _Action)
        {
            Debug.DebugLogs.WriteInLogs("[Plugin." + _Plugin.Name + "] <Action>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            ActionKeys.Add(_Plugin.Name + "." + _Name, Actions.Count());
            Actions.Add(Actions.Count, _Action);

        }

        #region Item

        public static Dictionary<string, int> ItemKeys = new Dictionary<string, int>();
        public static Dictionary<int, Core.GameObject.IItem> Items = new Dictionary<int, Core.GameObject.IItem>();

        public static void AddItem(this Plugin.IPlugin _Plugin, string _Name, Core.GameObject.IItem _Item)
        {
            Debug.DebugLogs.WriteInLogs("[Plugin." + _Plugin.Name + "] <Item>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            ItemKeys.Add(_Plugin.Name + "." + _Name, Items.Count());
            Items.Add(Items.Count, _Item);
            
        }

        #endregion


        #region Biome

        public static Dictionary<string, int> BiomeKey = new Dictionary<string, int>();
        public static Dictionary<int, Core.GameObject.Biome> Biomes = new Dictionary<int, Core.GameObject.Biome>();

        public static void AddBiome(this IPlugin _Plugin, string _Name, Core.GameObject.Biome _Biome)
        {
            Debug.DebugLogs.WriteInLogs("[Plugin." + _Plugin.Name + "] <Biome>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            BiomeKey.Add(_Plugin.Name + "." + _Name, Biomes.Count());
            Biomes.Add(Biomes.Count, _Biome);

        }

        #endregion

        #region Entities

        public static Dictionary<string, int> EntityKey = new Dictionary<string, int>();
        public static Dictionary<int, Core.GameObject.IEntity> Entities = new Dictionary<int, GameObject.IEntity>();

        public static void AddEntity(this IPlugin _Plugin, string _Name, Core.GameObject.IEntity _Entity)
        {
            Debug.DebugLogs.WriteInLogs("[Plugin." + _Plugin.Name + "] <Entity>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
            EntityKey.Add(_Plugin.Name + "." + _Name, Entities.Count());
            Entities.Add(Entities.Count, _Entity);

        }

        #endregion

        #region Tiles

        public static Dictionary<string, int> TileKeys = new Dictionary<string, int>();
        public static Dictionary<int, Core.GameObject.ITile> Tiles = new Dictionary<int, Core.GameObject.ITile>();

        public static void AddTile(this IPlugin _Plugin, string _Name, Core.GameObject.ITile _Tile)
        {
            Debug.DebugLogs.WriteInLogs("[Plugin." + _Plugin.Name + "] <Tile>" + _Plugin.Name + "." + _Name, Debug.LogType.Info);
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

            Debug.DebugLogs.WriteInLogs("[Plugin] Reloading...", Debug.LogType.Info);

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

        public static Dictionary<string, System.Reflection.Assembly> LoadedAssemblies = new Dictionary<string, System.Reflection.Assembly>();
        public static Dictionary<string, Plugin.IPlugin> Plugins = new Dictionary<string, Plugin.IPlugin>();

        public static void InitializePlugin()
        {
            if (IsLoaded == false)
            {

                //getting all plugin.
                foreach (string Dir in Directory.GetDirectories("Data"))
                {
                    
                    //Check if the main file existe.
                    if (File.Exists(Dir + "\\Main.cs") || File.Exists(Dir + "\\Main.vb"))
                    {

                        //Building file.
                        BuildOutput builderOutput = Builder.Build(Dir + "\\Main.cs", Dir + "\\Plugin.dll");
                        if (builderOutput.Sucess)
                        {

                            //Load Plugin
                            LoadedAssemblies.Add(Dir.Split('\\').Last(), builderOutput.Result.CompiledAssembly);
                            ICollection<Plugin.IPlugin> PluginCollection = Plugin.PluginLoader.LoadPlugin(builderOutput.Result.CompiledAssembly);


                            if (PluginCollection.Count == 0)
                            {

                                Debug.DebugLogs.WriteInLogs(Dir.Split('\\')[1] + " is not a plugin !", Debug.LogType.Warning, "Plugin");

                            }
                            else
                            {

                                foreach (Plugin.IPlugin i in PluginCollection)
                                {
                                    //try
                                    //{
                                        //Load All plugin
                                        Plugins.Add(i.Name, i);

                                        Debug.DebugLogs.WriteInLogs("[Plugin." + i.Name + "] Initializing...", Debug.LogType.Info);
                                        Plugins[i.Name].Initialize();
                                    //}
                                    //catch (Exception ex)
                                    //{

                                    //    Debug.Logs.Write("Failed to load " + i.Name + " !", Debug.LogType.Warning, "Plugin");
                                    //    System.Windows.Forms.MessageBox.Show(ex.ToString());
                                    //    throw;
                                    //}


                                }
                            }
                        }
                    }
                }
            }

            IsLoaded = true;
        }
        #endregion
    }
}
