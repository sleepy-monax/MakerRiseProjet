using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.MathExt;
using Maker.twiyol.GameObject;
using System;

namespace Maker.twiyol.Generator
{
    public class ChunkDecorator
    {

        Game.GameScene W;
        Random Rnd;
        public ChunkDecorator(Game.GameScene _World, Random _Rnd)
        {

            W = _World;
            Rnd = _Rnd;

        }

        public void Decorated(int cX, int cY, Game.WorldDataStruct.DataChunk Chunk)
        {

            Chunk.chunkStatut = Game.WorldDataStruct.chunkStatutList.onDecoration;


            DebugLogs.WriteInLogs("Generating " + cX + ":" + cY + "...", LogType.Info);

            for (int tX = 0; tX <= 15; tX++)
            {
                for (int tY = 0; tY <= 15; tY++)
                {

                    if (Chunk.Tiles[tX, tY].ID == -1)
                    {

                        Chunk.Tiles[tX, tY].ID = RandomHelper.GetRandomValueByWeight<int>(GameObjectManager.GetGameObject<Biome>(W.world.regions[Chunk.Tiles[tX, tY].Region].BiomeID).RandomTile, Rnd);
                        Chunk.Tiles[tX, tY].Variant = Rnd.Next(0, GameObjectManager.GetGameObject<ITile>(Chunk.Tiles[tX, tY].ID).MaxVariantCount);

                        W.miniMap.MiniMapBitmap.SetPixel(cX * 16 + tX, cY * 16 + tY, GameObjectManager.GetGameObject<ITile>(Chunk.Tiles[tX, tY].ID).MapColor);

                        if (Rnd.NextDouble() < GameObjectManager.GetGameObject<Biome>(W.world.regions[Chunk.Tiles[tX, tY].Region].BiomeID).EntityDensity)
                        {

                            int ID = RandomHelper.GetRandomValueByWeight<int>(GameObjectManager.GetGameObject<Biome>(W.world.regions[Chunk.Tiles[tX, tY].Region].BiomeID).RandomEntity, Rnd);
                            int Variant = Rnd.Next(0, GameObjectManager.GetGameObject<IEntity>(ID).MaxVariantCount + 1);

                            Chunk.AddEntity(new Game.WorldDataStruct.DataEntity(ID, Variant), new Microsoft.Xna.Framework.Point(tX, tY));


                        }
                    }


                }
            }

            Chunk.chunkStatut = Game.WorldDataStruct.chunkStatutList.Done;
            W.miniMap.RefreshMiniMap();
        }



    }
}
