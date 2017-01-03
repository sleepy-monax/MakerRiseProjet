namespace Maker.RiseEngine.Core.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        void Initialize();
        void OnWorldGeneration(twiyol.Game.GameScene world);
    }
}
