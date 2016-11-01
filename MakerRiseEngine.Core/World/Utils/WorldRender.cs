using RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace RiseEngine.Core.World.Utils
{
    public class WorldRender
    {

        WorldScene W;
        SpriteBatch TileSpriteBatch;
        SpriteBatch EntitySpriteBatch;


        public WorldRender(WorldScene _WorldScene)
        {
            W = _WorldScene;
            TileSpriteBatch = new SpriteBatch(Common.GraphicsDevice);
            EntitySpriteBatch = new SpriteBatch(Common.GraphicsDevice);
        }



        public void Draw(GameTime gameTime, bool Blur)
        {

            if (Blur)
            {
                TileSpriteBatch.Begin();
                EntitySpriteBatch.Begin();

            }
            else
            {

                TileSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                EntitySpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
            }

            for (int Ty = W.Camera.StartTile.Y; Ty <= W.Camera.EndTile.Y; Ty++)
            {
                for (int Tx = W.Camera.StartTile.X; Tx <= W.Camera.EndTile.X; Tx++)
                {
                    if (Tx >= 0 && Ty >= 0 && Tx < W.worldProperty.Size * 16 - 1 && Ty < W.worldProperty.Size * 16 - 1)
                    {

                        //Calcule des emplacements
                        Point CurrentLocation = new Point(Tx, Ty);
                        Point OnScreenLocation = new Point(
                            (Tx - W.Camera.StartTile.X) * W.Camera.Zoom + W.Camera.ScreenOrigine.X,
                             (Ty - W.Camera.StartTile.Y) * W.Camera.Zoom + W.Camera.ScreenOrigine.Y);

                        //Recuperation des arguments
                        GameObject.Event.GameObjectEventArgs e = W.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);

                        //recuperation des objets
                        WorldObj.ObjTile T = W.chunkManager.GetTile(CurrentLocation);


                        //desin des objets


                        GameObjectsManager.GetGameObject<GameObject.ITile>(T.ID).OnDraw(e, TileSpriteBatch, gameTime);



                        if (!(T.Entity == -1))
                        {
                            WorldObj.ObjEntity E = W.chunkManager.GetEntity(CurrentLocation);
                            GameObjectsManager.GetGameObject<GameObject.IEntity>(E.ID).OnDraw(e, EntitySpriteBatch, gameTime);

                        }

                    }




                }
            }
            TileSpriteBatch.End();
            EntitySpriteBatch.End();

        }
    }
}
