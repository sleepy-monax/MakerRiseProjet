using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RiseEngine.Core.Generator
{
    public class ChunkDecorator
    {

        World.WorldScene W;
        Random Rnd;
        public ChunkDecorator(World.WorldScene _World, Random _Rnd)
        {

            W = _World;
            Rnd = _Rnd;

        }

        public World.Obj.ObjChunk Decorated(int cX, int cY, World.Obj.ObjChunk Chunk)
        {

            Debug.DebugLogs.WriteInLogs("[ChunkDecorator] Generating " + cX + " : " + cY + " ...", Debug.LogType.Info);

            for (int tX = 0; tX <= 15; tX++)
            {
                for (int tY = 0; tY <= 15; tY++)
                {

                    if (Chunk.Tiles[tX, tY].ID == -1) {

                        Chunk.Tiles[tX, tY].ID = GameMath.RandomHelper.GetRandomValueByWeight<int>(GameObjectsManager.Biomes[W.Region[Chunk.Tiles[tX, tY].Region].BiomeID].RandomTile, Rnd);
                        Chunk.Tiles[tX, tY].Variant = Rnd.Next(0, GameObjectsManager.Tiles[Chunk.Tiles[tX, tY].ID].MaxVariantCount);

                        W.miniMap.MiniMapBitmap.SetPixel(cX * 16 + tX, cY * 16 + tY, GameObjectsManager.Tiles[Chunk.Tiles[tX, tY].ID].MapColor);

                        if (Rnd.NextDouble() < GameObjectsManager.Biomes[W.Region[Chunk.Tiles[tX, tY].Region].BiomeID].EntityDensity)
                        {

                            int ID = GameMath.RandomHelper.GetRandomValueByWeight<int>(GameObjectsManager.Biomes[W.Region[Chunk.Tiles[tX, tY].Region].BiomeID].RandomEntity, Rnd);
                            int Variant = Rnd.Next(0, GameObjectsManager.Entities[ID].MaxVariantCount);

                            Chunk.AddEntity(new World.Obj.ObjEntity(ID, Variant), new Microsoft.Xna.Framework.Point(tX, tY));


                        }
                    }
                    

                }
            }

            Chunk.IsDone = true;
            W.miniMap.RefreshMiniMap();
            W.saveFile.SaveChunk(cX, cY, Chunk);
            return Chunk;

        }



    }
}
