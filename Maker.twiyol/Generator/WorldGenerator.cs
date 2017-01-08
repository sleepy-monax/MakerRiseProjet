using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.MathExt;
using Maker.RiseEngine.Core.Plugin;
using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.Game.WorldDataStruct;
using Maker.twiyol.Scenes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Maker.twiyol.Generator
{
    public class WorldGenerator
    {

        WorldProperty WrldProps;
        RegionGenerator regionGenerator;
        System.Random Rnd;
        FastRandom FastRnd;

        public WorldGenerator(WorldProperty _WrldProps)
        {
            WrldProps = _WrldProps;
            Rnd = new System.Random(_WrldProps.Seed);
            FastRnd = new FastRandom(_WrldProps.Seed);
            regionGenerator = new RegionGenerator(this);
        }

        public Game.GameScene Generate()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            WorldGenerating sceneGen = new WorldGenerating();
            var game = (RiseEngine.Core.RiseEngine)Engine.MainGame;
            game.sceneManager.AddScene(sceneGen);
            sceneGen.show();

            Game.GameScene newGame = new Game.GameScene(WrldProps, Rnd);
            int maxWorldSize = WrldProps.Size * 16;
            Bitmap minimap = new Bitmap(maxWorldSize, maxWorldSize);
            int[,] regionGrid = new int[maxWorldSize, maxWorldSize];

            // Adding randome Region
            DebugLogs.WriteLog("Creating Random Point...", LogType.Info, "WorldGenerator");
            sceneGen.message = "Creation des régions...";
            Thread.Sleep(500);

            for (int rID = 1; rID <= WrldProps.regionCount; rID++)
            {
                // Get Random Region location.
                int x = FastRnd.Next(maxWorldSize);
                int y = FastRnd.Next(maxWorldSize);

                // Create the region.
                regionGenerator.GenerateRegion(rID, Location.ToWorldLocation(new Microsoft.Xna.Framework.Point(x, y)), newGame, Rnd);

                // Create the source tile.
                regionGrid.SetTile(x, y, rID);

                sceneGen.Progress = (int)((float)rID / WrldProps.regionCount * 100);
            }

            //expanding Region
            DebugLogs.WriteLog("Expending Region...", LogType.Info, "WorldGenerator");
            sceneGen.message = "Expansion des regions...";
            Thread.Sleep(500);

            for (int i = 0; i < WrldProps.RegionExpention; i++)
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

                sceneGen.Progress = (int)((float)i / WrldProps.RegionExpention * 100);
            }
            Thread.Sleep(100);
            sceneGen.Progress = 100;

            // Set loading message.
            DebugLogs.WriteLog("Converting Chunk... ", LogType.Info, "WorldGenerator");
            sceneGen.message = "Creation du Terrain...";
            Thread.Sleep(500);

            newGame.world.chunks = new DataChunk[WrldProps.Size, WrldProps.Size];

            for (int cX = 0; cX <= WrldProps.Size - 1; cX++)
            {
                for (int cY = 0; cY <= WrldProps.Size - 1; cY++)
                {
                    newGame.world.chunks[cX, cY] = new DataChunk();

                    for (int tX = 0; tX <= 15; tX++)
                    {
                        for (int tY = 0; tY <= 15; tY++)
                        {
                            newGame.world.chunks[cX, cY].Tiles[tX, tY] = new DataTile();
                            newGame.world.chunks[cX, cY].Tiles[tX, tY].Region = regionGrid[cX * 16 + tX, cY * 16 + tY];

                        }
                    }


                    sceneGen.Progress = (int)((float)(cX + cY) / WrldProps.Size * 2 * 100);
                }
            }

            newGame.miniMap.MiniMapBitmap = minimap;
            newGame.miniMap.RefreshMiniMap();

            // Raising onWorldGeneration event on plugin.
            //foreach (KeyValuePair<string, IPlugin> i in GameObjectManager.Plugins)
            //{
            //    i.Value.OnWorldGeneration(newGame);
            //}

            stopwatch.Stop();
            DebugLogs.WriteLog("Generator elapsed time : " + stopwatch.ElapsedMilliseconds, LogType.Info, "WorldGenerator");

            game.sceneManager.RemoveScene(sceneGen);

            return newGame;
        }

    }
}
