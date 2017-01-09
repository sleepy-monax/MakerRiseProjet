namespace Maker.RiseEngine.Core.GameObject
{
    public interface IGameObject
    {
        string GameObjectName { get; set; }
        string pluginName { get; set; }

        void OnGameObjectAdded();
    }
}
