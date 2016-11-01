using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiseEngine.Core.GameObject
{
    public class Biome : IGameObject
    {
        public string gameObjectName { get; set; }
        public GameMath.KeyWeightPair<int>[] RandomEntity;
        public GameMath.KeyWeightPair<int>[] RandomTile;
        public double EntityDensity { get; set; }

        public Biome(string _Name, double _EntityDensity, GameMath.KeyWeightPair<int>[] _RandomEntity, GameMath.KeyWeightPair<int>[] _RandomTile)
        {

            
            gameObjectName = _Name;

            EntityDensity = _EntityDensity;
            RandomEntity = _RandomEntity;
            RandomTile = _RandomTile;


        }

    }
}
 