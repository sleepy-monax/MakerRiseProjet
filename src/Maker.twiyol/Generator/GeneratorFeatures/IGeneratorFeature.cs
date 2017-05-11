

using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameObjects;

namespace Maker.Twiyol.Generator.GeneratorFeatures
{
    interface IGeneratorFeature : IGameObject
    {

        void OnRegionCreation(int[,] regionGrid);
        void OnTerrainCreation();
        void OnChunkDecoration();

    }
}
