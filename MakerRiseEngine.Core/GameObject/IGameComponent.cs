namespace Maker.RiseEngine.Core.GameComponent
{
    public interface IGameObject
    {
        string GameObjectName { get; set; }
        string PluginName { get; set; }

        void OnGameObjectAdded();
    }
}
