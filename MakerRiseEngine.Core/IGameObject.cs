namespace Maker.RiseEngine.Core
{
    public interface IGameObject
    {
        string gameObjectName { get; set; }
        string pluginName { get; set; }

        void OnGameObjectAdded();
    }
}
