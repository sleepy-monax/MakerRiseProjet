using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiseEngine.Core;
using RiseEngine.Core.World.Obj;
using System.Drawing;
using RiseEngine.Core.World.Utils;
using System.Diagnostics;

namespace RiseEngine.Core.Generator
{
    public class WorldGenerator
    {

        World.Utils.WorldProperty WrldProps;

        RegionGenerator regionGenerator;
        Random Rnd;
        GameMath.FastRandom FastRnd;


        public WorldGenerator(World.Utils.WorldProperty _WrldProps) {
            WrldProps = _WrldProps;
            Rnd = new Random(_WrldProps.Seed);
            FastRnd = new GameMath.FastRandom(_WrldProps.Seed);
            regionGenerator = new RegionGenerator(this);
            

        }

        public World.WorldScene Generate() {

            Stopwatch stw = new Stopwatch();
            stw.Start();
            World.WorldScene NewWorld = new World.WorldScene(WrldProps, Rnd);
            

            int MaxWorldSize = WrldProps.Size * 16;
            //debug;
            Bitmap Map = new System.Drawing.Bitmap(MaxWorldSize, MaxWorldSize);

            int[,] rGrid = new int[MaxWorldSize, MaxWorldSize];

            //Adding randome Region
            Debug.DebugLogs.WriteInLogs("[WorldGenerator] Creating Random Point", Debug.LogType.Info);
            for (int rID = 1; rID <= WrldProps.regionCount; rID++)
            {

                int x = FastRnd.Next(MaxWorldSize);
                int y = FastRnd.Next(MaxWorldSize);

                regionGenerator.GenerateRegion(rID, Location.ToWorldLocation(new Microsoft.Xna.Framework.Point(x,y)), NewWorld, Rnd);

                PutPixel(rGrid, Map, x, y, rID);
                Map.SetPixel(x, y, Color.Red);
            }

            //expanding Region

            Debug.DebugLogs.WriteInLogs("[WorldGenerator] Expending Region", Debug.LogType.Info);

            bool DoLoop = true;
            int LoopCount = 0;
            do
            {
                
                for (int x = 0; x <= MaxWorldSize - 1; x++)
                {
                    for (int y = 0; y <= MaxWorldSize - 1; y++)
                    {

                        if (!(rGrid[x, y] == 0)) {

                            int RegionID = rGrid[x, y];
                            int Direction = FastRnd.Next(0, 5);

                            switch (Direction)
                            {
                                case 0:

                                    //do nothing

                                    break;
                                case 1:

                                    
                                    PutPixel(rGrid, Map, x - 1, y, RegionID);

                                    break;
                                case 2:

                                    
                                    PutPixel(rGrid, Map, x + 1, y, RegionID);

                                    break;
                                case 3:

                                    PutPixel(rGrid, Map, x, y - 1, RegionID);

                                    break;
                                case 4:

                                    PutPixel(rGrid, Map, x, y + 1, RegionID);

                                    break;
                                default:
                                    break;
                            }

                        }

                    }
                }

                //exit Loop
                if (LoopCount == WrldProps.RegionExpention) DoLoop = false;

                LoopCount++;
            } while (DoLoop);


           

            Debug.DebugLogs.WriteInLogs("[WorldGenerator] Creating Chunk", Debug.LogType.Info);
            NewWorld.Chunks = new ObjChunk[WrldProps.Size, WrldProps.Size];

            for (int cX = 0; cX <= WrldProps.Size - 1; cX++)
            {
                for (int cY = 0; cY <= WrldProps.Size - 1; cY++)
                {

                    NewWorld.Chunks[cX, cY] = new ObjChunk();

                    for (int tX = 0; tX <= 15; tX++)
                    {
                        for (int tY = 0; tY <= 15; tY++)
                        {
                            NewWorld.Chunks[cX, cY].Tiles[tX, tY] = new World.Obj.ObjTile();
                            NewWorld.Chunks[cX, cY].Tiles[tX, tY].Region = rGrid[cX * 16 + tX, cY * 16 + tY];

                        }
                    }

                }
            }

            NewWorld.miniMap.MiniMapBitmap = Map;



            Map.Save("World.png");
            NewWorld.miniMap.RefreshMiniMap();

            foreach (KeyValuePair<string, Plugin.IPlugin> i in GameObjectsManager.Plugins) {
                i.Value.OnWorldGeneration(NewWorld);
            }

            stw.Stop();
            Debug.DebugLogs.WriteInLogs("Generator elapsed time : " + stw.ElapsedMilliseconds);

            return NewWorld;

        }


        public static int[,] PutPixel(int[,] Grid,Bitmap bitmap, int x, int y, int Value) {

            if (x < 0) return Grid;
            if (y < 0) return Grid;

            if (x > Grid.GetLength(0) - 1) return Grid;
            if (y > Grid.GetLength(1) - 1) return Grid;

            if (Grid[x, y] == 0)
            {
                Grid[x, y] = Value;
                bitmap.SetPixel(x, y, Color.FromArgb(Value, Value, Value));
            }

            return Grid;
        }

    }
}
