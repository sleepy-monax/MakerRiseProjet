namespace Maker.RiseEngine.Core.GameObject
{
    public interface IGameObject
    {
        string GameObjectName { get; set; }
        string PluginName { get; set; }

        void OnGameObjectAdded();
    }
}
