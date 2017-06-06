

using Maker.RiseEngine;
using Maker.RiseEngine.GameObjects;

namespace Maker.Twiyol.Generator.GeneratorFeatures
{
    interface IGeneratorFeature : IGameObject
    {

        void OnRegionCreation(int[,] regionGrid);
        void OnTerrainCreation();
        void OnChunkDecoration();

    }
}
