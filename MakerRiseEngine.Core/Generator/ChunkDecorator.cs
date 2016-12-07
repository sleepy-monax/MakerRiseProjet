using Maker.RiseEngine.Core.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Maker.RiseEngine.Core.Generator
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

        public void Decorated(int cX, int cY, World.WorldObj.ObjChunk Chunk)
        {

            Chunk.chunkStatut = World.WorldObj.chunkStatutList.onDecoration;


            EngineDebug.DebugLogs.WriteInLogs("Generating " + cX + " : " + cY + " ...", EngineDebug.LogType.Info);

            for (int tX = 0; tX <= 15; tX++)
            {
                for (int tY = 0; tY <= 15; tY++)
                {

                    if (Chunk.Tiles[tX, tY].ID == -1)
                    {
                        
                        Chunk.Tiles[tX, tY].ID = GameMath.RandomHelper.GetRandomValueByWeight<int>(GameObjectsManager.GetGameObject<Biome>(W.Region[Chunk.Tiles[tX, tY].Region].BiomeID).RandomTile, Rnd);
                        Chunk.Tiles[tX, tY].Variant = Rnd.Next(0, GameObjectsManager.GetGameObject<ITile>(Chunk.Tiles[tX, tY].ID).MaxVariantCount);

                        W.miniMap.MiniMapBitmap.SetPixel(cX * 16 + tX, cY * 16 + tY, GameObjectsManager.GetGameObject<ITile>(Chunk.Tiles[tX, tY].ID).MapColor);

                        if (Rnd.NextDouble() < GameObjectsManager.GetGameObject<Biome>(W.Region[Chunk.Tiles[tX, tY].Region].BiomeID).EntityDensity)
                        {

                            int ID = GameMath.RandomHelper.GetRandomValueByWeight<int>(GameObjectsManager.GetGameObject<Biome>(W.Region[Chunk.Tiles[tX, tY].Region].BiomeID).RandomEntity, Rnd);
                            int Variant = Rnd.Next(0, GameObjectsManager.GetGameObject<IEntity>(ID).MaxVariantCount);

                            Chunk.AddEntity(new World.WorldObj.ObjEntity(ID, Variant), new Microsoft.Xna.Framework.Point(tX, tY));


                        }
                    }


                }
            }

            Chunk.chunkStatut = World.WorldObj.chunkStatutList.Done;
            W.miniMap.RefreshMiniMap();
            W.saveFile.SaveChunk(cX, cY, Chunk);


        }



    }
}
