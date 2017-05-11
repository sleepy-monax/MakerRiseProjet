using Maker.RiseEngine.Core.GameObjects;
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

            DataRegion NewRegion = new DataRegion();
            NewRegion.Name = "Region_" + ID;
            NewRegion.Origine = Location;
            NewRegion.BiomeID = GameComponentManager.GetGameObjectIndex(GameObject.Biome.Biomes[rnd.Next(GameObject.Biome.Biomes.Count)]);
            //NewRegion.Color = new Color(rnd.Next(256), rnd.Next(256), rnd.Next(256));


            world.regions.Add(ID, NewRegion);

        }

    }
}
