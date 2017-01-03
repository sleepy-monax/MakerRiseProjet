using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.MathExt;
using System.Collections.Generic;

namespace Maker.twiyol.GameObject
{
    public class Biome : IGameObject
    {
        public string gameObjectName { get; set; }
        public string pluginName { get; set; }

        public KeyWeightPair<int>[] RandomEntity;
        public KeyWeightPair<int>[] RandomTile;
        public double EntityDensity { get; set; }

        public static List<string> Biomes = new List<string>();

        public Biome(double _EntityDensity, KeyWeightPair<int>[] _RandomEntity, KeyWeightPair<int>[] _RandomTile)
        {

            EntityDensity = _EntityDensity;
            RandomEntity = _RandomEntity;
            RandomTile = _RandomTile;

            

        }

        public void OnGameObjectAdded()
        {
            Biomes.Add(pluginName + '.' + gameObjectName);
        }
    }
}
 