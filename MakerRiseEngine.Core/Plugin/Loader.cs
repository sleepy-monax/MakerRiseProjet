using System;
using System.Collections.Generic;
using System.Reflection;

namespace Maker.RiseEngine.Core.Plugin
{
    public static class PluginLoader
    {

        public static ICollection<T> LoadAssembly<T>(Assembly assembly)
        {
                EngineDebug.DebugLogs.WriteInLogs("load \'" + assembly.CodeBase + "\'", EngineDebug.LogType.Info, "Plugin.Loader");

                Type pluginType = typeof(T);
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

                ICollection<T> plugins = new List<T>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    T plugin = (T)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
        }

    }
}
