namespace Maker.RiseEngine.Core.Plugin
{
    public interface IPlugin
    {
        string PluginName { get; }
        void Initialize<PluginType>(PluginLoader<PluginType> pluginLoader, engine ENGINE) where PluginType : IPlugin;
    }
}
