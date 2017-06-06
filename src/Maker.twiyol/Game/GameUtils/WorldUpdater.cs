using Maker.RiseEngine;
using Maker.RiseEngine.GameObjects;
using Maker.RiseEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Twiyol.Game.GameUtils
{
    public class WorldUpdater
    {
        GameScene G;

        public WorldUpdater(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        #region Update

        public void Update(GameInput playerInput, GameTime gameTime)
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
                            (Tx - G.Camera.StartTile.X) * G.Camera.TileUnit + G.Camera.ScreenOrigine.X,
                            (Ty - G.Camera.StartTile.Y) * G.Camera.TileUnit + G.Camera.ScreenOrigine.Y);

                        if (G.chunkDecorator.PrepareChunk(CurrentLocation.ToWorldLocation().chunkX, CurrentLocation.ToWorldLocation().chunkY))
                        {
                            //recuperation des arguments
                            GameObject.Event.GameObjectEventArgs e = G.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), OnScreenLocation);

                            //recuperation des objets
                            WorldDataStruct.DataTile T = G.World.GetTile(CurrentLocation);

                            GameObjectManager.GetGameObject<GameObject.ITile>(T.ID).Tick(e, gameTime);
                            GameObjectManager.GetGameObject<GameObject.ITile>(T.ID).Update(e, playerInput, gameTime);

                            if (!(T.Entity == -1))
                            {
                                //On recuper l'entitée
                                WorldDataStruct.DataEntity E = G.World.GetEntity(CurrentLocation);
                                E.Location = CurrentLocation.ToWorldLocation();

                                if (e.ParrentEntity.Tags.HasTag("attack_cooldown") && e.ParrentEntity.Tags.GetTag("attack_cooldown", 0) > 0) {
                                    e.ParrentEntity.Tags.SetTag("attack_cooldown", (e.ParrentEntity.Tags.GetTag("attack_cooldown", 1) - 1) );
                                }

                                GameObjectManager.GetGameObject<GameObject.IEntity>(E.ID).Tick(e, gameTime);
                                GameObjectManager.GetGameObject<GameObject.IEntity>(E.ID).Update(e, playerInput, gameTime);
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
