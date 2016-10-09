using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.World.Utils
{
    public class WorldUpdater : BaseObject
    {
        WorldScene W;

        Point CurrentLocation;
        Point OnScreenLocation;

        public WorldUpdater(WorldScene _WorldScene)
        {
            W = _WorldScene;
        }

        #region Update

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            for (int Tx = W.Camera.StartTile.X; Tx <= W.Camera.EndTile.X; Tx++)
            {
                for (int Ty = W.Camera.StartTile.Y; Ty <= W.Camera.EndTile.Y; Ty++)
                {
                    if (Tx >= 0 && Ty >= 0 && Tx < W.worldProperty.Size * 16 - 1 && Ty < W.worldProperty.Size * 16 - 1)
                    {

                        //Calcule des emplacements
                        CurrentLocation = new Point(Tx, Ty);
                        OnScreenLocation = new Point(
                            (Tx - W.Camera.StartTile.X) * W.Camera.Zoom + W.Camera.ScreenOrigine.X, 
                            (Ty - W.Camera.StartTile.Y) * W.Camera.Zoom + W.Camera.ScreenOrigine.Y);

                        //recuperation des arguments
                        GameObject.Event.GameObjectEventArgs e = W.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);

                        //recuperation des objets
                        Obj.ObjTile T = W.chunkManager.GetTile(CurrentLocation);

                        GameObjectsManager.Tiles[T.ID].OnTick(e, gameTime);
                        GameObjectsManager.Tiles[T.ID].OnUpdate(e, KeyBoard, Mouse, gameTime);

                        if (!(T.Entity == -1))
                        {
                            //On recuper l'entitée
                            Obj.ObjEntity E = W.chunkManager.GetEntity(CurrentLocation);
                            E.Location = CurrentLocation.ToWorldLocation();

                            GameObjectsManager.Entities[E.ID].OnTick(e, gameTime);
                            GameObjectsManager.Entities[E.ID].OnUpdate(e, KeyBoard, Mouse, gameTime);
                        }

                    }

                }
            }

            base.Update(Mouse, KeyBoard, gameTime);
        }
        #endregion

    }
}
