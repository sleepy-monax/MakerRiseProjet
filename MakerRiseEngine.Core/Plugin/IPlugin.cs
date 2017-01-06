namespace Maker.RiseEngine.Core.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        void Initialize<PluginType>(PluginLoader<PluginType> pluginLoader) where PluginType : IPlugin;
    }
}
