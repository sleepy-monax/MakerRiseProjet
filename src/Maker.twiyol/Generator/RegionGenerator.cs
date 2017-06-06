using Maker.RiseEngine.GameObjects;
using Maker.Twiyol.Game.GameUtils;
using Maker.Twiyol.Game.WorldDataStruct;
using Microsoft.Xna.Framework;
using System;

namespace Maker.Twiyol.Generator
{
    public class RegionGenerator
    {

        WorldGenerator worldGenerator;

        public RegionGenerator(WorldGenerator _WorldGenerator)
        {

            worldGenerator = _WorldGenerator;

        }

        public void GenerateRegion(int ID, WorldLocation Location, DataWorld world, Random rnd)
        {
            DataRegion NewRegion = new DataRegion()
            {
                Name = "Region_" + ID,
                Origine = Location,
                BiomeID = GameObjectManager.GetGameObjectIndex(GameObject.Biome.Biomes[rnd.Next(GameObject.Biome.Biomes.Count)])
            };

            world.regions.Add(ID, NewRegion);
        }
    }
}
