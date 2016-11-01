using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RiseEngine.Core.World.Utils;

namespace RiseEngine.Core.Generator
{
    public class RegionGenerator
    {

        WorldGenerator worldGenerator;

        public RegionGenerator(WorldGenerator _WorldGenerator) {

            worldGenerator = _WorldGenerator;

        }

        public void GenerateRegion(int ID,WorldLocation Location, World.WorldScene NewWorld, Random rnd) {

            World.WorldObj.ObjRegion NewRegion = new World.WorldObj.ObjRegion();
            NewRegion.Name = "Region_" + ID;
            NewRegion.Origine = Location;
            NewRegion.BiomeID = rnd.Next(GameObjectsManager.Biomes.Count);
            NewRegion.Color = new Color(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            

            NewWorld.Region.Add(ID, NewRegion);

        }

    }
}
