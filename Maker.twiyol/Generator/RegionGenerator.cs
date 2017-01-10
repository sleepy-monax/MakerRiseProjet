﻿using Maker.RiseEngine.Core.GameObject;
using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.Game.WorldDataStruct;
using Microsoft.Xna.Framework;
using System;

namespace Maker.twiyol.Generator
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
            NewRegion.BiomeID = GameObjectManager.GetGameObjectIndex(GameObject.Biome.Biomes[rnd.Next(GameObject.Biome.Biomes.Count)]);
            //NewRegion.Color = new Color(rnd.Next(256), rnd.Next(256), rnd.Next(256));


            world.regions.Add(ID, NewRegion);

        }

    }
}
