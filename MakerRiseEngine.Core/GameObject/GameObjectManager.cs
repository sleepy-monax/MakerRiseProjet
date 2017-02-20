using Maker.RiseEngine.Core.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.GameComponent
{
    public static class GameComponentManager
    {

        static List<IGameObject> gameObject = new List<IGameObject>();
        static Dictionary<string, int> gameObjectDict = new Dictionary<string, int>();

        public static void AddGameObject(this IPlugin plugin, string gameObjectName, IGameObject gameObject)
        {
            int gameObjectID = gameObject.Count;

            gameObject.GameObjectName = gameObjectName;
            gameObject.PluginName = plugin.Name;

            gameObject.Add(_gameObject);
            gameObjectDict.Add(plugin.Name + '.' + gameObjectName, gameObjectID);

            gameObject.OnGameObjectAdded();
        }

        public static int GetGameObjectIndex(string gameObjectName)
        {
            string[] Names = gameObjectName.Split('.');
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

    }
}
