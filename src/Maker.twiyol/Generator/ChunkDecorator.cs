using Maker.RiseEngine.EngineDebug;
using Maker.RiseEngine.GameObjects;
using Maker.RiseEngine.MathExt;
using Maker.Twiyol.Game.WorldDataStruct;
using Maker.Twiyol.GameObject;
using System;

namespace Maker.Twiyol.Generator
{
    public class ChunkDecorator
    {

        Game.GameScene G;
        Random Random;
        public ChunkDecorator(Game.GameScene game, Random random)
        {

            G = game;
            Random = random;

        }

        public bool PrepareChunk( int x, int y)
        {

            DataChunk chunk = G.World.chunks[x, y];

            switch (chunk.chunkStatut)
            {
                case chunkStatutList.Done:
                    return true;
                case chunkStatutList.onDecoration:
                    return false;
                case chunkStatutList.needDecoration:
                    Decorated(x, y, G.World.chunks[x, y]);
                    return false;
                default:
                    break;
            }

            return false;
        }

        public void Decorated(int cX, int cY, DataChunk Chunk)
        {

            Chunk.chunkStatut = chunkStatutList.onDecoration;


            Debug.WriteLog("Generating " + cX + ":" + cY + "...", LogType.Info);

            for (int tX = 0; tX <= 15; tX++)
            {
                for (int tY = 0; tY <= 15; tY++)
                {

                    if (Chunk.Tiles[tX, tY].ID == -1)
                    {

                        Chunk.Tiles[tX, tY].ID = RandomHelper.GetRandomValueByWeight<int>(GameObjectManager.GetGameObject<Biome>(G.World.regions[Chunk.Tiles[tX, tY].Region].BiomeID).RandomTile, Random);
                        Chunk.Tiles[tX, tY].Variant = Random.Next(0, GameObjectManager.GetGameObject<ITile>(Chunk.Tiles[tX, tY].ID).MaxVariantCount);

                        //G.World.WorldBitmap.SetPixel(cX * 16 + tX, cY * 16 + tY, GameObjectManager.GetGameObject<ITile>(Chunk.Tiles[tX, tY].ID).MapColor);

                        if (Random.NextDouble() < GameObjectManager.GetGameObject<Biome>(G.World.regions[Chunk.Tiles[tX, tY].Region].BiomeID).Rule.GetValue(cX*16 + tX, cY*16 + tY))
                        {

                            int ID = RandomHelper.GetRandomValueByWeight<int>(GameObjectManager.GetGameObject<Biome>(G.World.regions[Chunk.Tiles[tX, tY].Region].BiomeID).RandomEntity, Random);
                            int Variant = Random.Next(0, GameObjectManager.GetGameObject<IEntity>(ID).MaxVariantCount + 1);

                            Chunk.AddEntity(new DataEntity(ID, Variant), new Microsoft.Xna.Framework.Point(tX, tY));


                        }
                    }


                }
            }

            Chunk.chunkStatut = chunkStatutList.Done;
            G.miniMap.RefreshMiniMap();
        }



    }
}
