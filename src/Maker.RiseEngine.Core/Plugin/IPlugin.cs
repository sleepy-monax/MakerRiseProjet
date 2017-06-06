namespace Maker.RiseEngine.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        void Initialize(PluginLoader pluginLoader, GameEngine engine);
    }
}
