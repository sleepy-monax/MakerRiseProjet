using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.twiyol.Game.GameUtils
{
    public class WorldRender
    {

        GameScene G;
        SpriteBatch tSpriteBatch;
        SpriteBatch eSpriteBatch;
        

        public WorldRender(GameScene _WorldScene)
        {
            G = _WorldScene;
            tSpriteBatch = new SpriteBatch(Engine.GraphicsDevice);
            eSpriteBatch = new SpriteBatch(Engine.GraphicsDevice);
        }



        public void Draw(RenderTarget2D r,GameTime gameTime)
        {
            Engine.GraphicsDevice.SetRenderTarget(r);
            Engine.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            tSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            eSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);


            for (int Ty = G.Camera.StartTile.Y; Ty <= G.Camera.EndTile.Y; Ty++)
            {
                for (int Tx = G.Camera.StartTile.X; Tx <= G.Camera.EndTile.X; Tx++)
                {
                    if (Tx >= 0 && Ty >= 0 && Tx < G.World.Size * 16 - 1 && Ty < G.World.Size * 16 - 1)
                    {

                        //Calcule des emplacements
                        Point CurrentLocation = new Point(Tx, Ty);
                        Point OnScreenLocation = new Point(
                            (Tx - G.Camera.StartTile.X) * G.Camera.TileUnit + G.Camera.ScreenOrigine.X,
                             (Ty - G.Camera.StartTile.Y) * G.Camera.TileUnit + G.Camera.ScreenOrigine.Y);

                        if (G.chunkDecorator.PrepareChunk(CurrentLocation.ToWorldLocation().chunkX, CurrentLocation.ToWorldLocation().chunkY))
                        {

                            //Recuperation des arguments
                            GameObject.Event.GameObjectEventArgs e = G.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);

                            //recuperation des objets
                            WorldDataStruct.DataTile T = G.World.GetTile(CurrentLocation);


                            //desin des objets


                            GameObjectManager.GetGameObject<GameObject.ITile>(T.ID).OnDraw(e, tSpriteBatch, gameTime);



                            if (!(T.Entity == -1))
                            {
                                WorldDataStruct.DataEntity E = G.World.GetEntity(CurrentLocation);
                                GameObjectManager.GetGameObject<GameObject.IEntity>(E.ID).OnDraw(e, eSpriteBatch, gameTime);

                                if (Engine.engineConfig.Debug_WorldOverDraw && E.IsFocus)
                                {

                                    eSpriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), $"ID : {E.ID}\nV : {E.Variant}", OnScreenLocation.ToVector2() + new Vector2(2, 2) + E.GetOnTileLocation(), Color.Black);
                                    eSpriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), $"ID : {E.ID}\nV : {E.Variant}", OnScreenLocation.ToVector2() + E.GetOnTileLocation(), Color.White);

                                }
                            }
                        }
                    }
                }
            }

            // Terminate SpriteBatch.
            tSpriteBatch.End();
            eSpriteBatch.End();

            Engine.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
