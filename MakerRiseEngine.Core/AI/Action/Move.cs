using Maker.RiseEngine.Core.Game.GameUtils;
using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.GameObject.Event;
using Microsoft.Xna.Framework;

namespace Maker.RiseEngine.Core.AI.Action
{
    public class Move : IAction
    {
        public string gameObjectName
        {
            get;
            set;
        }

        public string pluginName
        {
            get;
            set;
        }

        public void OnGameObjectAdded()
        {
            
        }

        public void Performe(GameObjectEventArgs e, GameTime gametime)
        {
            e.ParrentEntity.ActionProgress += GameObjectsManager.GetGameObject<IEntity>(e.ParrentEntity.ID).MoveSpeed;
            WorldLocation FocusLocation = e.CurrentLocation.AddPoint(e.ParrentEntity.Facing.ToPoint());

            if (!(e.World.entityManager.TileIsFree(FocusLocation)))
            {
                e.ParrentEntity.Action = -1;
                e.ParrentEntity.ActionProgress = 0;
                e.ParrentEntity.OnTileLocation = Vector2.Zero;
            }
            else
            {
                if (e.ParrentEntity.ActionProgress >= 100)
                {
                    e.ParrentEntity.ActionProgress = 0;
                    e.ParrentEntity.OnTileLocation = Vector2.Zero;
                    e.World.entityManager.MoveEntity(e.CurrentLocation, FocusLocation);
                    e.ParrentEntity.Action = -1;

                    GameObjectsManager.GetGameObject<ITile>(e.ParrentTile.ID).OnEntityWalkIn(e, gametime);
                }
                else
                {
                    e.ParrentEntity.OnTileLocation = e.ParrentEntity.Facing.ToVector2(e.ParrentEntity.ActionProgress);
                }

                if (e.ParrentEntity.IsFocus)
                {
                    e.World.Camera.FocusLocation = e.ParrentEntity.Location.ToPoint();
                    e.World.Camera.PreciseFocusLocation = e.ParrentEntity.OnTileLocation;
                }
            }
        }
    }
}
