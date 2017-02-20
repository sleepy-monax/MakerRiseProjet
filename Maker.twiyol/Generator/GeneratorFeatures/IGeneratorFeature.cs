

using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameComponent;

namespace Maker.twiyol.Generator.GeneratorFeatures
{
    interface IGeneratorFeature : IGameObject
    {

        void OnRegionCreation(int[,] regionGrid);
        void OnTerrainCreation();
        void OnChunkDecoration();

    }
}
