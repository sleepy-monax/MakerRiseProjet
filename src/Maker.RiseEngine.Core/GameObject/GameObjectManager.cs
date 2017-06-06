using Maker.RiseEngine.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.GameObjects
{
    public static class GameObjectManager
    {

        static List<IGameObject> GameObject = new List<IGameObject>();
        static Dictionary<string, int> GameObjectDict = new Dictionary<string, int>();

        public static int AddGameObject(this IPlugin plugin, string gameObjectName, IGameObject gameObject)
        {
            int gameObjectID = GameObject.Count;

            gameObject.GameObjectName = gameObjectName;
            gameObject.PluginName = plugin.Name;

            GameObject.Add(gameObject);
            GameObjectDict.Add(plugin.Name + '.' + gameObjectName, gameObjectID);

            gameObject.OnGameObjectAdded();

            return gameObjectID;
        }

        public static int GetGameObjectIndex(string gameObjectName)
        {
            string[] Names = gameObjectName.Split('.');
            return GetGameObjectIndex(Names[0], Names[1]);
        }

        public static T GetGameObject<T>(int index) where T : IGameObject
        {
            if (0 <= index && index <= GameObject.Count())
            {
                return (T)GameObject[index];
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
            if (GameObjectDict.ContainsKey(pluginName + '.' + gameObjectName))
            {
                return GameObjectDict[pluginName + '.' + gameObjectName];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

    }
}
