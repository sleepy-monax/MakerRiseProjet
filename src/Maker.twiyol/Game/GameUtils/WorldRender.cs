using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.GameObjects;
using Maker.RiseEngine.Core.Rendering;
using Maker.Twiyol.Game.WorldDataStruct;
using Maker.Twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Twiyol.Game.GameUtils
{
    public class WorldRender
    {

        GameScene G;
        SpriteBatch tSpriteBatch;
        SpriteBatch eSpriteBatch;
        

        public WorldRender(GameScene _WorldScene)
        {
            G = _WorldScene;
            tSpriteBatch = new SpriteBatch(rise.GraphicsDevice);
            eSpriteBatch = new SpriteBatch(rise.GraphicsDevice);
        }



        public void Draw(RenderTarget2D r,GameTime gameTime)
        {
            rise.GraphicsDevice.SetRenderTarget(r);
            rise.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

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
                            GameObjectEventArgs e = G.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);

                            //recuperation des objets
                            DataTile T = G.World.GetTile(CurrentLocation);


                            //desin des objets
                            GameComponentManager.GetGameObject<GameObject.ITile>(T.ID).Draw(e, tSpriteBatch, gameTime);

                            if (!(T.Entity == -1))
                            {
                                DataEntity E = G.World.GetEntity(CurrentLocation);
                                Vector2 onTileOffset = (E.GetOnTileOffset() * G.Camera.TileUnit);

                                GameComponentManager.GetGameObject<GameObject.IEntity>(E.ID).Draw(e, eSpriteBatch, gameTime);

                                if (E.Tags.HasTag("attack_cooldown") && E.Tags.GetTag("attack_cooldown", 0) > 0)
                                {
                                    eSpriteBatch.FillRectangle(new Rectangle(OnScreenLocation.X + G.Camera.TileUnit / 2 - 51 + (int)onTileOffset.X, OnScreenLocation.Y - 21 + (int)onTileOffset.Y, 102, 12), new Color(Color.Black, 0.5f));
                                    eSpriteBatch.FillRectangle(new Rectangle(OnScreenLocation.X + G.Camera.TileUnit / 2 - 50 + (int)onTileOffset.X, OnScreenLocation.Y - 20 + (int)onTileOffset.Y, (int)(E.Tags.GetTag("attack_cooldown", 0) * 3.33f), 10), Color.White);
                                }

                                if (E.Tags.HasTag("heal") && E.Tags.GetTag("heal", 0) < E.ToGameObject().MaxHeal) {
                                    eSpriteBatch.FillRectangle(new Rectangle(OnScreenLocation.X + G.Camera.TileUnit / 2 - 51 + (int)onTileOffset.X, OnScreenLocation.Y - 31 + (int)onTileOffset.Y, 102, 12), new Color(Color.Black, 0.5f));
                                    eSpriteBatch.FillRectangle(new Rectangle(OnScreenLocation.X + G.Camera.TileUnit / 2 - 50 + (int)onTileOffset.X, OnScreenLocation.Y - 30 + (int)onTileOffset.Y, E.Tags.GetTag("heal", 0) * (100 / E.ToGameObject().MaxHeal), 10), Color.DarkRed);
                                }

                               
                                if (rise.engineConfig.Debug_WorldOverDraw && E.IsCameraFocus)
                                {

                                    eSpriteBatch.DrawString(G.ENGINE.RESSOUCES.SpriteFont("Engine", "Consolas_16pt"), $"ID : {E.ID}\nV : {E.Variant}", OnScreenLocation.ToVector2() + (E.GetOnTileOffset() * G.Camera.TileUnit), Color.White);

                                }
                            }
                        }
                    }
                }
            }

            // Terminate SpriteBatch.
            tSpriteBatch.End();
            eSpriteBatch.End();

            rise.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
