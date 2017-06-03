using Maker.RiseEngine.Core;

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
        GameScene gameScene;
        SpriteBatch tileSpriteBatch;
        SpriteBatch entitySpritebatch;
        GameEngine Engine;

        public WorldRender(GameEngine engine, GameScene worldScene)
        {
            Engine = engine;
            gameScene = worldScene;
            tileSpriteBatch = new SpriteBatch(Engine.GraphicsDevice);
            entitySpritebatch = new SpriteBatch(Engine.GraphicsDevice);
        }

        public void DrawWorld(RenderTarget2D renderTarget, GameTime gameTime)
        {
            Engine.GraphicsDevice.SetRenderTarget(renderTarget);
            Engine.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            tileSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            entitySpritebatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            for (int Ty = gameScene.Camera.StartTile.Y; Ty <= gameScene.Camera.EndTile.Y; Ty++)
            {
                for (int Tx = gameScene.Camera.StartTile.X; Tx <= gameScene.Camera.EndTile.X; Tx++)
                {
                    if (Tx >= 0 && Ty >= 0 && Tx < gameScene.World.Size * 16 - 1 && Ty < gameScene.World.Size * 16 - 1)
                    {
                        Point CurrentLocation = new Point(Tx, Ty);
                        Point OnScreenLocation = new Point(
                            (Tx - gameScene.Camera.StartTile.X) * gameScene.Camera.TileUnit + gameScene.Camera.ScreenOrigine.X,
                            (Ty - gameScene.Camera.StartTile.Y) * gameScene.Camera.TileUnit + gameScene.Camera.ScreenOrigine.Y);

                        if (gameScene.chunkDecorator.PrepareChunk(CurrentLocation.ToWorldLocation().chunkX, CurrentLocation.ToWorldLocation().chunkY))
                        {
                            GameObjectEventArgs e = gameScene.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);
                            DataTile tileData = gameScene.World.GetTile(CurrentLocation);
                            GameObjectManager.GetGameObject<GameObject.ITile>(tileData.ID).Draw(e, tileSpriteBatch, gameTime);

                            if (!(tileData.Entity == -1))
                            {
                                DataEntity E = gameScene.World.GetEntity(CurrentLocation);
                                Vector2 onTileOffset = (E.GetOnTileOffset() * gameScene.Camera.TileUnit);

                                GameObjectManager.GetGameObject<GameObject.IEntity>(E.ID).Draw(e, entitySpritebatch, gameTime);

                                if (E.Tags.HasTag("attack_cooldown") && E.Tags.GetTag("attack_cooldown", 0) > 0)
                                {
                                    entitySpritebatch.FillRectangle(new Rectangle(OnScreenLocation.X + gameScene.Camera.TileUnit / 2 - 51 + (int)onTileOffset.X, OnScreenLocation.Y - 21 + (int)onTileOffset.Y, 102, 12), new Color(Color.Black, 0.5f));
                                    entitySpritebatch.FillRectangle(new Rectangle(OnScreenLocation.X + gameScene.Camera.TileUnit / 2 - 50 + (int)onTileOffset.X, OnScreenLocation.Y - 20 + (int)onTileOffset.Y, (int)(E.Tags.GetTag("attack_cooldown", 0) * 3.33f), 10), Color.White);
                                }

                                if (E.Tags.HasTag("heal") && E.Tags.GetTag("heal", 0) < E.ToGameObject().MaxHeal) {
                                    entitySpritebatch.FillRectangle(new Rectangle(OnScreenLocation.X + gameScene.Camera.TileUnit / 2 - 51 + (int)onTileOffset.X, OnScreenLocation.Y - 31 + (int)onTileOffset.Y, 102, 12), new Color(Color.Black, 0.5f));
                                    entitySpritebatch.FillRectangle(new Rectangle(OnScreenLocation.X + gameScene.Camera.TileUnit / 2 - 50 + (int)onTileOffset.X, OnScreenLocation.Y - 30 + (int)onTileOffset.Y, E.Tags.GetTag("heal", 0) * (100 / E.ToGameObject().MaxHeal), 10), Color.DarkRed);
                                }
                            }
                        }
                    }
                }
            }

            tileSpriteBatch.End();
            entitySpritebatch.End();

            Engine.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
