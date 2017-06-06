using Maker.RiseEngine.EngineDebug;
using Maker.RiseEngine.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace Maker.RiseEngine.Plugin
{
    public class PluginLoader
    {
        public Dictionary<string, IPlugin> Plugins;
        List<string> LoadedPlugins;
        List<string> OnIntializationPlugin;
        GameEngine Engine;

        public PluginLoader(GameEngine engine, string pluginProfil) {
            Plugins = new Dictionary<string, IPlugin>();
            LoadedPlugins = new List<string>();
            OnIntializationPlugin = new List<string>();
            Engine = engine;

            foreach (IPlugin p in LoadPluginProfil(pluginProfil)) {

                Plugins.Add(p.Name, p);
         
            }
        }
        

        public void Include(object Parent, string pluginName) {

            var plug = Plugins[pluginName];

            if (OnIntializationPlugin.Contains(pluginName))
                Debug.WriteLog($"A circular dependency has been detected! {Parent.GetType().Name} refers to {pluginName}, which makes itself reference to {Parent.GetType().Name}.", LogType.Error, nameof(PluginLoader));
            else if (!LoadedPlugins.Contains(pluginName))
            {
                Debug.WriteLog("Load pluging :" + plug.GetType().Name, LogType.Info, nameof(PluginLoader));
                OnIntializationPlugin.Add(pluginName);
                plug.Initialize(this, Engine);
                OnIntializationPlugin.Remove(pluginName);

                LoadedPlugins.Add(pluginName);
            }
        }

        public void InitializePlugin() {

            foreach (var plug in Plugins)
            {

                Debug.WriteLog("Initializing " + plug.Key, LogType.Info, nameof(PluginLoader));
                Include(this, plug.Key);

            }
        }

        private List<IPlugin> LoadPluginProfil(string profil)
        {
            List<IPlugin> loadedPlugin = new List<IPlugin>();
            string listPath = $"Plugins\\{profil}.rise";
            
            if (File.Exists(listPath))
            {
                List<string> pluginToLoad = ListSheet.ParseListSheet(listPath);

                foreach (var pluginName in pluginToLoad)
                {
                    Debug.WriteLog($"Loading plugin: {pluginName}", LogType.Info, nameof(PluginLoader));
                    loadedPlugin.AddRange(LoadPlugin($"Plugins\\{pluginName}"));
                }
            }
            else
            {
                Debug.WriteLog($"PluginProfil not found: {profil}!", LogType.Warning, nameof(PluginLoader));
            }

            return loadedPlugin;
        }

        private List<IPlugin> LoadPlugin(string path)
        {
            List<IPlugin> LoadedPlugin = new List<IPlugin>();
            string metaFilePath = path + "\\plugin.risedata";

            if (File.Exists(metaFilePath))
            {
                DataSheet pluginMetaData = new DataSheet(metaFilePath);

                string plugin_Path = pluginMetaData.GetData("Path");
                string assemblie_path = path + '\\' + plugin_Path;

                if (File.Exists(assemblie_path))
                {
                    Assembly pluginAsm = Assembly.LoadFrom(Environment.CurrentDirectory + '\\' + assemblie_path);
                    LoadedPlugin.AddRange(LoadPluginAssemblie(pluginAsm));
                }
                else
                    Debug.WriteLog($"Plugin assembly not found: {plugin_Path}", LogType.Warning, nameof(PluginLoader));
            }
            else
                Debug.WriteLog($"No plugin metadata found: {metaFilePath}", LogType.Warning, nameof(PluginLoader));

            return LoadedPlugin;
        }

        private List<IPlugin> LoadPluginAssemblie(Assembly assembly)
        {
            Debug.WriteLog($"load '{assembly.FullName}'", LogType.Info, nameof(PluginLoader));

            Type pluginType = typeof(IPlugin);
            List<Type> pluginTypes = new List<Type>();

            if (assembly != null)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (type.IsInterface || type.IsAbstract)
                        continue;
                    else if (type.GetInterface(pluginType.FullName) != null)
                        pluginTypes.Add(type);
                }
            }

            List<IPlugin> plugins = new List<IPlugin>(pluginTypes.Count);
            foreach (Type type in pluginTypes)
            {
                IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                plugins.Add(plugin);
            }

            return plugins;
        }

    }
}
