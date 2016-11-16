using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maker.RiseEngine.Core.GameObject
{
    public class Biome : IGameObject
    {
        public string gameObjectName { get; set; }
        public string pluginName { get; set; }

        public GameMath.KeyWeightPair<int>[] RandomEntity;
        public GameMath.KeyWeightPair<int>[] RandomTile;
        public double EntityDensity { get; set; }

        public static List<string> Biomes = new List<string>();

        public Biome(double _EntityDensity, GameMath.KeyWeightPair<int>[] _RandomEntity, GameMath.KeyWeightPair<int>[] _RandomTile)
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
 