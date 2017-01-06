
using Maker.RiseEngine.Core.GameObject;
using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.GameObject;
using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;

namespace Maker.twiyol.AI.Action
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
            e.ParrentEntity.ActionProgress += GameObjectManager.GetGameObject<IEntity>(e.ParrentEntity.ID).MoveSpeed;
            WorldLocation FocusLocation = e.CurrentLocation.AddPoint(e.ParrentEntity.Facing.ToPoint());

            if (!(e.World.EntityDataManager.IsEntityFree(FocusLocation)))
            {
                e.ParrentEntity.Action = -1;
                e.ParrentEntity.ActionProgress = 0;
                e.ParrentEntity.SetOnTileLocation(Vector2.Zero);
            }
            else
            {
                if (e.ParrentEntity.ActionProgress >= 100)
                {
                    e.ParrentEntity.ActionProgress = 0;
                    e.ParrentEntity.SetOnTileLocation(Vector2.Zero);
                    e.World.EntityDataManager.MoveEntity(e.CurrentLocation, FocusLocation);
                    e.ParrentEntity.Action = -1;
                    GameObjectManager.GetGameObject<ITile>(e.ParrentTile.ID).OnEntityWalkIn(e, gametime);
                }
                else
                {
                    e.ParrentEntity.SetOnTileLocation(e.ParrentEntity.Facing.ToVector2(e.ParrentEntity.ActionProgress));
                }

                if (e.ParrentEntity.IsFocus)
                {
                    e.World.Camera.FocusLocation = e.ParrentEntity.Location.ToPoint();
                    e.World.Camera.PreciseFocusLocation = e.ParrentEntity.GetOnTileLocation();
                }
            }
        }
    }
}
