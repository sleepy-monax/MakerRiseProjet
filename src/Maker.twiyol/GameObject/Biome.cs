using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameObjects;
using Maker.RiseEngine.Core.MathExt;
using Maker.Twiyol.Generator.EntitiesDistribution;
using System.Collections.Generic;

namespace Maker.Twiyol.GameObject
{
    public class Biome : IGameObject
    {
        public string GameObjectName { get; set; }
        public string PluginName { get; set; }

        public KeyWeightPair<int>[] RandomEntity;
        public KeyWeightPair<int>[] RandomTile;
        public IEntitiesDistributionRule Rule;

        public static List<string> Biomes = new List<string>();

        public Biome(IEntitiesDistributionRule rule, KeyWeightPair<int>[] _RandomEntity, KeyWeightPair<int>[] _RandomTile)
        {
            Rule = rule;
            RandomEntity = _RandomEntity;
            RandomTile = _RandomTile;
        }

        public void OnGameObjectAdded()
        {
            Biomes.Add(PluginName + '.' + GameObjectName);
        }
    }
}
 