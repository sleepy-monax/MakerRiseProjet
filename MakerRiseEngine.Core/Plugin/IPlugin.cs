namespace Maker.RiseEngine.Core.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        void Initialize();
        void OnWorldGeneration(Game.GameScene world);
    }
}
