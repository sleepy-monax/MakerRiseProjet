using Maker.RiseEngine.Core.GameObject;

namespace Maker.RiseEngine.Core.Generator.GeneratorFeatures
{
    interface IGeneratorFeature : IGameObject
    {

        void onRegionCreation();
        void onChunkDecoration();

    }
}
