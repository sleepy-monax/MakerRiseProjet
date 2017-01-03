using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Maker.twiyol.Game.GameUtils
{
    public class WorldUpdater : Idrawable
    {
        GameScene G;

        public WorldUpdater(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        #region Update

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            for (int Tx = G.Camera.StartTile.X; Tx <= G.Camera.EndTile.X; Tx++)
            {
                for (int Ty = G.Camera.StartTile.Y; Ty <= G.Camera.EndTile.Y; Ty++)
                {
                    if (Tx >= 0 && Ty >= 0 && Tx < G.worldProperty.Size * 16 - 1 && Ty < G.worldProperty.Size * 16 - 1)
                    {


                        //Calcule des emplacements
                        Point CurrentLocation = new Point(Tx, Ty);
                        Point OnScreenLocation = new Point(
                            (Tx - G.Camera.StartTile.X) * G.Camera.Zoom + G.Camera.ScreenOrigine.X,
                            (Ty - G.Camera.StartTile.Y) * G.Camera.Zoom + G.Camera.ScreenOrigine.Y);

                        if (G.chunkManager.PrepareChunk(CurrentLocation.ToWorldLocation().chunkX, CurrentLocation.ToWorldLocation().chunkY))
                        {
                            //recuperation des arguments
                            GameObject.Event.GameObjectEventArgs e = G.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);

                            //recuperation des objets
                            WorldDataStruct.DataTile T = G.chunkManager.GetTile(CurrentLocation);

                            GameObjectsManager.GetGameObject<GameObject.ITile>(T.ID).OnTick(e, gameTime);
                            GameObjectsManager.GetGameObject<GameObject.ITile>(T.ID).OnUpdate(e, KeyBoard, Mouse, gameTime);

                            if (!(T.Entity == -1))
                            {
                                //On recuper l'entitée
                                WorldDataStruct.DataEntity E = G.chunkManager.GetEntity(CurrentLocation);
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
