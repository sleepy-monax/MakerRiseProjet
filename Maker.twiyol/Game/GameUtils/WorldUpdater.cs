using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.twiyol.Game.GameUtils
{
    public class WorldUpdater : RiseEngine.Core.IDrawable
    {
        GameScene G;

        public WorldUpdater(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        #region Update

        public void Update(PlayerInput playerInput, GameTime gameTime)
        {

            for (int Tx = G.Camera.StartTile.X; Tx <= G.Camera.EndTile.X; Tx++)
            {
                for (int Ty = G.Camera.StartTile.Y; Ty <= G.Camera.EndTile.Y; Ty++)
                {
                    if (Tx >= 0 && Ty >= 0 && Tx < G.World.Size * 16 - 1 && Ty < G.World.Size * 16 - 1)
                    {


                        //Calcule des emplacements
                        Point CurrentLocation = new Point(Tx, Ty);
                        Point OnScreenLocation = new Point(
                            (Tx - G.Camera.StartTile.X) * G.Camera.Zoom + G.Camera.ScreenOrigine.X,
                            (Ty - G.Camera.StartTile.Y) * G.Camera.Zoom + G.Camera.ScreenOrigine.Y);

                        if (G.chunkDecorator.PrepareChunk(CurrentLocation.ToWorldLocation().chunkX, CurrentLocation.ToWorldLocation().chunkY))
                        {
                            //recuperation des arguments
                            GameObject.Event.GameObjectEventArgs e = G.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);

                            //recuperation des objets
                            WorldDataStruct.DataTile T = G.World.GetTile(CurrentLocation);

                            GameObjectManager.GetGameObject<GameObject.ITile>(T.ID).OnTick(e, gameTime);
                            GameObjectManager.GetGameObject<GameObject.ITile>(T.ID).OnUpdate(e, playerInput, gameTime);

                            if (!(T.Entity == -1))
                            {
                                //On recuper l'entitée
                                WorldDataStruct.DataEntity E = G.World.GetEntity(CurrentLocation);
                                E.Location = CurrentLocation.ToWorldLocation();

                                GameObjectManager.GetGameObject<GameObject.IEntity>(E.ID).OnTick(e, gameTime);
                                GameObjectManager.GetGameObject<GameObject.IEntity>(E.ID).OnUpdate(e, playerInput, gameTime);
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
