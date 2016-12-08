using System;
using System.Collections.Generic;
using System.Reflection;

namespace Maker.RiseEngine.Core.Plugin
{
    public static class PluginLoader
    {

        public static ICollection<IPlugin> LoadPlugin(Assembly assembly)
        {
                EngineDebug.DebugLogs.WriteInLogs("load \'" + assembly.CodeBase + "\'", EngineDebug.LogType.Info, "Plugin.Loader");

                Type pluginType = typeof(IPlugin);
                ICollection<Type> pluginTypes = new List<Type>();

                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            if (type.GetInterface(pluginType.FullName) != null)
                            {
                                pluginTypes.Add(type);
                            }
                        }
                    }
                }

                ICollection<IPlugin> plugins = new List<IPlugin>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
        }

    }
}
