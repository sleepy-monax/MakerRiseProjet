namespace Maker.RiseEngine.Core.GameObjects
{
    public interface IGameObject
    {
        string GameObjectName { get; set; }
        string PluginName { get; set; }

        void OnGameObjectAdded();
    }
}
