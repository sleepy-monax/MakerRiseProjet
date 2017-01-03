using Maker.twiyol.Game.GameUtils;
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

        public void GenerateRegion(int ID, WorldLocation Location, Game.GameScene newGame, Random rnd)
        {

            Game.WorldDataStruct.DataRegion NewRegion = new Game.WorldDataStruct.DataRegion();
            NewRegion.Name = "Region_" + ID;
            NewRegion.Origine = Location;
            NewRegion.BiomeID = GameObjectsManager.GetGameObjectIndex(GameObject.Biome.Biomes[rnd.Next(GameObject.Biome.Biomes.Count)]);
            //NewRegion.Color = new Color(rnd.Next(256), rnd.Next(256), rnd.Next(256));


            newGame.world.regions.Add(ID, NewRegion);

        }

    }
}
