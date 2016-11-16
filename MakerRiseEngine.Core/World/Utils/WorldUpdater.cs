using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Core.World.Utils
{
    public class WorldUpdater : Idrawable
    {
        WorldScene W;

        public WorldUpdater(WorldScene _WorldScene)
        {
            W = _WorldScene;
        }

        #region Update

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            for (int Tx = W.Camera.StartTile.X; Tx <= W.Camera.EndTile.X; Tx++)
            {
                for (int Ty = W.Camera.StartTile.Y; Ty <= W.Camera.EndTile.Y; Ty++)
                {
                    if (Tx >= 0 && Ty >= 0 && Tx < W.worldProperty.Size * 16 - 1 && Ty < W.worldProperty.Size * 16 - 1)
                    {


                        //Calcule des emplacements
                        Point CurrentLocation = new Point(Tx, Ty);
                        Point OnScreenLocation = new Point(
                            (Tx - W.Camera.StartTile.X) * W.Camera.Zoom + W.Camera.ScreenOrigine.X,
                            (Ty - W.Camera.StartTile.Y) * W.Camera.Zoom + W.Camera.ScreenOrigine.Y);

                        if (W.chunkManager.PrepareChunk(CurrentLocation.ToWorldLocation().chunk.X, CurrentLocation.ToWorldLocation().chunk.Y))
                        {
                            //recuperation des arguments
                            GameObject.Event.GameObjectEventArgs e = W.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);

                            //recuperation des objets
                            WorldObj.ObjTile T = W.chunkManager.GetTile(CurrentLocation);

                            GameObjectsManager.GetGameObject<GameObject.ITile>(T.ID).OnTick(e, gameTime);
                            GameObjectsManager.GetGameObject<GameObject.ITile>(T.ID).OnUpdate(e, KeyBoard, Mouse, gameTime);

                            if (!(T.Entity == -1))
                            {
                                //On recuper l'entitée
                                WorldObj.ObjEntity E = W.chunkManager.GetEntity(CurrentLocation);
                                E.Location = CurrentLocation.ToWorldLocation();

                                GameObjectsManager.GetGameObject<GameObject.IEntity>(E.ID).OnTick(e, gameTime);
                                GameObjectsManager.GetGameObject<GameObject.IEntity>(E.ID).OnUpdate(e, KeyBoard, Mouse, gameTime);
                            }
                        }
                    }

                }
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
