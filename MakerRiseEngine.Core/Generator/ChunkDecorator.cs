using Maker.RiseEngine.Core.GameObject;
using System;

namespace Maker.RiseEngine.Core.Generator
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

        public void Decorated(int cX, int cY, Game.World.ObjChunk Chunk)
        {

            Chunk.chunkStatut = Game.World.chunkStatutList.onDecoration;


            EngineDebug.DebugLogs.WriteInLogs("Generating " + cX + " : " + cY + " ...", EngineDebug.LogType.Info);

            for (int tX = 0; tX <= 15; tX++)
            {
                for (int tY = 0; tY <= 15; tY++)
                {

                    if (Chunk.Tiles[tX, tY].ID == -1)
                    {
                        
                        Chunk.Tiles[tX, tY].ID = MathExt.RandomHelper.GetRandomValueByWeight<int>(GameObjectsManager.GetGameObject<Biome>(W.world.regions[Chunk.Tiles[tX, tY].Region].BiomeID).RandomTile, Rnd);
                        Chunk.Tiles[tX, tY].Variant = Rnd.Next(0, GameObjectsManager.GetGameObject<ITile>(Chunk.Tiles[tX, tY].ID).MaxVariantCount);

                        W.miniMap.MiniMapBitmap.SetPixel(cX * 16 + tX, cY * 16 + tY, GameObjectsManager.GetGameObject<ITile>(Chunk.Tiles[tX, tY].ID).MapColor);

                        if (Rnd.NextDouble() < GameObjectsManager.GetGameObject<Biome>(W.world.regions[Chunk.Tiles[tX, tY].Region].BiomeID).EntityDensity)
                        {

                            int ID = MathExt.RandomHelper.GetRandomValueByWeight<int>(GameObjectsManager.GetGameObject<Biome>(W.world.regions[Chunk.Tiles[tX, tY].Region].BiomeID).RandomEntity, Rnd);
                            int Variant = Rnd.Next(0, GameObjectsManager.GetGameObject<IEntity>(ID).MaxVariantCount);

                            Chunk.AddEntity(new Game.World.ObjEntity(ID, Variant), new Microsoft.Xna.Framework.Point(tX, tY));


                        }
                    }


                }
            }

            Chunk.chunkStatut = Game.World.chunkStatutList.Done;
            W.miniMap.RefreshMiniMap();
            W.saveFile.SaveChunk(cX, cY, Chunk);


        }



    }
}
