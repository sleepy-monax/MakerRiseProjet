namespace Maker.RiseEngine.Core.Plugin
{
    public interface IPlugin
    {
        string PluginName { get; }
        void Initialize(PluginLoader pluginLoader, GameEngine engine);
    }
}
