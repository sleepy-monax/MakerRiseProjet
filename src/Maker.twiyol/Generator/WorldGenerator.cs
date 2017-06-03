using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.GameObjects;
using Maker.RiseEngine.Core.MathExt;
using Maker.RiseEngine.Core.Plugin;
using Maker.Twiyol.Events;
using Maker.Twiyol.Game.GameUtils;
using Maker.Twiyol.Game.WorldDataStruct;
using Maker.Twiyol.Generator.GeneratorFeatures;
using Maker.Twiyol.Scenes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Maker.Twiyol.Generator
{
    public class WorldGenerator
    {

        GeneratorProperty GeneratorProperty;
        RegionGenerator regionGenerator;
        System.Random Rnd;
        FastRandom FastRnd;

        private List<int> GeneratorFeatures;

        public WorldGenerator(GeneratorProperty generatorProperty)
        {
            GeneratorProperty = generatorProperty;
            Rnd = new System.Random(generatorProperty.Seed);
            FastRnd = new FastRandom(generatorProperty.Seed);
            regionGenerator = new RegionGenerator(this);
            GeneratorFeatures = new List<int>();
        }

        public DataWorld Generate()
        {
            // Start the stop watch to count generation delta time.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Create the World instance.
            DataWorld newWorld = new DataWorld(GeneratorProperty.WorldName, GeneratorProperty.Seed, GeneratorProperty.WorldSize);

            // Raise OnWorldGneratingBegin.
            GameEventHandler.RaiseOnWorldGeneratingBegin(this, newWorld, this);

            // Show the loading scene.
            WorldGenerating sceneGen = new WorldGenerating();
            var game = Rise.Engine;
            game.sceneManager.AddScene(sceneGen);
            sceneGen.Show();

            int maxWorldSize = GeneratorProperty.WorldSize * 16;
            int[,] regionGrid = new int[maxWorldSize, maxWorldSize];

            // Adding randome Region
            DebugLogs.WriteLog("Creating Random Point...", LogType.Info, "WorldGenerator");
            sceneGen.message = "Creation des régions...";
            Thread.Sleep(500);

            for (int rID = 0; rID <= GeneratorProperty.MaxRegionCount; rID++)
            {
                // Get Random Region location.
                int x = FastRnd.Next(maxWorldSize);
                int y = FastRnd.Next(maxWorldSize);

                // Create the region.
                regionGenerator.GenerateRegion(rID, Location.ToWorldLocation(new Microsoft.Xna.Framework.Point(x, y)), newWorld, Rnd);

                // Create the source tile.
                regionGrid.SetTile(x, y, rID);

                sceneGen.Progress = (int)((float)rID / GeneratorProperty.MaxRegionCount * 100);
            }

            //expanding Region
            DebugLogs.WriteLog("Expending Region...", LogType.Info, "WorldGenerator");
            sceneGen.message = "Expansion des regions...";
            Thread.Sleep(500);

            for (int i = 0; i < GeneratorProperty.RegionExpention; i++)
            {
                for (int x = 0; x <= maxWorldSize - 1; x++)
                {
                    for (int y = 0; y <= maxWorldSize - 1; y++)
                    {

                        if (!(regionGrid[x, y] == 0))
                        {

                            int RegionID = regionGrid[x, y];
                            int Direction = FastRnd.Next(0, 5);

                            // Placing Pixel at coordinate.
                            switch (Direction)
                            {
                                case 1:
                                    regionGrid.SetTile(x - 1, y, RegionID);
                                    break;
                                case 2:
                                    regionGrid.SetTile(x + 1, y, RegionID);
                                    break;
                                case 3:
                                    regionGrid.SetTile(x, y - 1, RegionID);

                                    break;
                                case 4:
                                    regionGrid.SetTile(x, y + 1, RegionID);
                                    break;
                            }

                        }

                    }
                }

                sceneGen.Progress = (int)((float)i / GeneratorProperty.RegionExpention * 100);
            }
            Thread.Sleep(100);
            sceneGen.Progress = 100;



            // Apply generator features.
            DebugLogs.WriteLog("Apply features...", LogType.Info, "WorldGenerator");
            sceneGen.message = "Apply features...";

            foreach (int id in GeneratorFeatures)
            {
                GameObjectManager.GetGameObject<IGeneratorFeature>(id).OnRegionCreation(regionGrid);
            }

            // Set loading message.
            DebugLogs.WriteLog("Converting Chunk... ", LogType.Info, "WorldGenerator");
            sceneGen.message = "Creation du Terrain...";
            Thread.Sleep(500);

            newWorld.chunks = new DataChunk[GeneratorProperty.WorldSize, GeneratorProperty.WorldSize];

            for (int cX = 0; cX <= GeneratorProperty.WorldSize - 1; cX++)
            {
                for (int cY = 0; cY <= GeneratorProperty.WorldSize - 1; cY++)
                {
                    newWorld.chunks[cX, cY] = new DataChunk();

                    for (int tX = 0; tX <= 15; tX++)
                    {
                        for (int tY = 0; tY <= 15; tY++)
                        {
                            newWorld.chunks[cX, cY].Tiles[tX, tY] = new DataTile();
                            newWorld.chunks[cX, cY].Tiles[tX, tY].Region = regionGrid[cX * 16 + tX, cY * 16 + tY];

                        }
                    }


                    sceneGen.Progress = (int)((float)(cX + cY) / GeneratorProperty.WorldSize * 2 * 100);
                }
            }

            // Creating world bitmap.
            Bitmap minimap = new Bitmap(maxWorldSize, maxWorldSize);
            newWorld.WorldBitmap = minimap;

            GameEventHandler.RaiseOnWorldGeneratingEnd(this, newWorld, this);

            stopwatch.Stop();
            DebugLogs.WriteLog("Generator elapsed time : " + stopwatch.ToString(), LogType.Info, "WorldGenerator");

            game.sceneManager.RemoveScene(sceneGen);

            return newWorld;
        }

    }
}
