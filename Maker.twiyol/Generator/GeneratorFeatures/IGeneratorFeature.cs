﻿

using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameObject;

namespace Maker.twiyol.Generator.GeneratorFeatures
{
    interface IGeneratorFeature : IGameObject
    {

        void onRegionCreation();
        void onChunkDecoration();

    }
}
