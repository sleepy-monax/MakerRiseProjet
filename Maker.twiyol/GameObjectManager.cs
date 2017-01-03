using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.Plugin;
using Maker.twiyol.GameObject;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Maker.twiyol
{


    public static class GameObjectsManager
    {

        static bool IsLoaded = false;

        #region GameObjects
        static List<IGameObject> gameObject = new List<IGameObject>();
        static Dictionary<string, int> gameObjectDict = new Dictionary<string, int>();

        public static void AddGameObject(this IPlugin plugin, string gameObjectName, IGameObject _gameObject)
        {
            int gameObjectID = gameObject.Count;

            _gameObject.gameObjectName = gameObjectName;
            _gameObject.pluginName = plugin.Name;

            gameObject.Add(_gameObject);
            gameObjectDict.Add(plugin.Name + '.' + gameObjectName, gameObjectID);

            _gameObject.OnGameObjectAdded();
        }

        internal static int GetGameObjectIndex(string gameObjectID)
        {
            string[] Names = gameObjectID.Split('.');
            return GetGameObjectIndex(Names[0], Names[1]);
        }

        public static T GetGameObject<T>(int index) where T : IGameObject
        {
            if (0 <= index && index <= gameObject.Count())
            {
                return (T)gameObject[index];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public static T GetGameObject<T>(this IPlugin plugin, string gameObjectName) where T : IGameObject
        {
            return GetGameObject<T>(plugin.Name, gameObjectName);
        }
        public static T GetGameObject<T>(string pluginName, string gameObjectName) where T : IGameObject
        {
            return GetGameObject<T>(GetGameObjectIndex(pluginName, gameObjectName));
        }

        public static int GetGameObjectIndex(this IPlugin plugin, string gameObjectName)
        {

            return GetGameObjectIndex(plugin.Name, gameObjectName);

        }

        public static int GetGameObjectIndex(string pluginName, string gameObjectName)
        {
            if (gameObjectDict.ContainsKey(pluginName + '.' + gameObjectName))
            {
                return gameObjectDict[pluginName + '.' + gameObjectName];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        #endregion

        #region  security

        public static bool IsFullLoaded()
        {
            if (gameObject.Count() != 0)
                return true;
            return false;
        }

        #endregion

        public static void Reload()
        {
            DebugLogs.WriteInLogs("Reloading...", LogType.Info, "Plugin");

            IsLoaded = false;

            gameObject.Clear();
            gameObjectDict.Clear();
        }

        #region Plugin

        public static Dictionary<string, System.Reflection.Assembly> LoadedAssemblies = new Dictionary<string, System.Reflection.Assembly>();
        public static Dictionary<string, IPlugin> Plugins = new Dictionary<string, IPlugin>();

        public static void LoadPlugins()
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
                            ICollection<IPlugin> PluginCollection = PluginLoader.LoadAssembly<IPlugin>(builderOutput.Result.CompiledAssembly);

                            if (PluginCollection.Count == 0)
                            {

                                DebugLogs.WriteInLogs(Dir.Split('\\')[1] + " is not a plugin !", LogType.Warning, "Plugin");

                            }
                            else
                            {

                                foreach (IPlugin i in PluginCollection)
                                {
                                    Plugins.Add(i.Name, i);
                                    DebugLogs.WriteInLogs("Initializing...", LogType.Info, "Plugin." + i.Name);
                                    Plugins[i.Name].Initialize();
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
