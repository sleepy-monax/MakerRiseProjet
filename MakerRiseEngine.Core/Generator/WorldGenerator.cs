using Maker.RiseEngine.Core.Game.GameUtils;
using Maker.RiseEngine.Core.Game.World;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Maker.RiseEngine.Core.Generator
{
    public class WorldGenerator
    {

        WorldProperty WrldProps;
        RegionGenerator regionGenerator;
        Random Rnd;
        MathExt.FastRandom FastRnd;

        public WorldGenerator(WorldProperty _WrldProps)
        {
            WrldProps = _WrldProps;
            Rnd = new Random(_WrldProps.Seed);
            FastRnd = new MathExt.FastRandom(_WrldProps.Seed);
            regionGenerator = new RegionGenerator(this);
        }

        public Game.GameScene Generate()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Game.GameScene newGame = new Game.GameScene(WrldProps, Rnd);
            int maxWorldSize = WrldProps.Size * 16;
            Bitmap minimap = new Bitmap(maxWorldSize, maxWorldSize);
            int[,] regionGrid = new int[maxWorldSize, maxWorldSize];

            // Adding randome Region
            EngineDebug.DebugLogs.WriteInLogs("Creating Random Point...", EngineDebug.LogType.Info, "WorldGenerator");
            Scene.SceneManager.WG.message = "Creating Random Point...";
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
            }

            //expanding Region
            EngineDebug.DebugLogs.WriteInLogs("Expending Region...", EngineDebug.LogType.Info, "WorldGenerator");
            Scene.SceneManager.WG.message = "Expending Region...";
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
            }

            // Set loading message.
            EngineDebug.DebugLogs.WriteInLogs("Converting Chunk... ", EngineDebug.LogType.Info, "WorldGenerator");
            Scene.SceneManager.WG.message = "Converting Chunk...";
            Thread.Sleep(500);

            newGame.world.chunks = new ObjChunk[WrldProps.Size, WrldProps.Size];

            for (int cX = 0; cX <= WrldProps.Size - 1; cX++)
            {
                for (int cY = 0; cY <= WrldProps.Size - 1; cY++)
                {
                    newGame.world.chunks[cX, cY] = new ObjChunk();

                    for (int tX = 0; tX <= 15; tX++)
                    {
                        for (int tY = 0; tY <= 15; tY++)
                        {
                            newGame.world.chunks[cX, cY].Tiles[tX, tY] = new Game.World.ObjTile();
                            newGame.world.chunks[cX, cY].Tiles[tX, tY].Region = regionGrid[cX * 16 + tX, cY * 16 + tY];

                        }
                    }

                }
            }

            newGame.miniMap.MiniMapBitmap = minimap;
            newGame.miniMap.RefreshMiniMap();

            // Raising onWorldGeneration event on plugin.
            foreach (KeyValuePair<string, Plugin.IPlugin> i in GameObjectsManager.Plugins)
            {
                i.Value.OnWorldGeneration(newGame);
            }

            stopwatch.Stop();
            EngineDebug.DebugLogs.WriteInLogs("Generator elapsed time : " + stopwatch.ElapsedMilliseconds, EngineDebug.LogType.Info, "WorldGenerator");

            return newGame;
        }

    }
}
