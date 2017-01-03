

using Maker.RiseEngine.Core;

namespace Maker.twiyol.Generator.GeneratorFeatures
{
    interface IGeneratorFeature : IGameObject
    {

        void onRegionCreation();
        void onChunkDecoration();

    }
}
