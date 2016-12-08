namespace Maker.RiseEngine.Core.GameObject
{
    public interface IGameObject
    {
        string gameObjectName { get; set; }
        string pluginName { get; set; }

        void OnGameObjectAdded();
    }
}
