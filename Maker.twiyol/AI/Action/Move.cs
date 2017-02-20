
using Maker.RiseEngine.Core.GameComponent;
using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.GameObject;
using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;

namespace Maker.twiyol.AI.Action
{
    public class Move : IAction
    {
        public string GameObjectName
        {
            get;
            set;
        }

        public string PluginName
        {
            get;
            set;
        }

        public void OnGameObjectAdded()
        {

        }

        public void Performe(GameObjectEventArgs e, GameTime gametime)
        {
            e.ParrentEntity.Tags.SetTag("move_progress", e.ParrentEntity.Tags.GetTag("move_progress", 0) + GameComponentManager.GetGameObject<IEntity>(e.ParrentEntity.ID).MoveSpeed);
            WorldLocation DestinationLocation = e.CurrentLocation.AddPoint(e.ParrentEntity.Tags.GetTag("facing", Facing.Down).ToPoint());

            if (!(e.Game.World.IsEntityFree(DestinationLocation)))
            {
                e.ParrentEntity.Tags.SetTag("ai_action", -1);
                e.ParrentEntity.Tags.SetTag("move_progress", 0);
                e.ParrentEntity.SetOnTileOffset(Vector2.Zero);
            }
            else
            {
                if (e.ParrentEntity.Tags.GetTag("move_progress", 100) >= 100)
                {
                    e.ParrentEntity.Tags.SetTag("move_progress", 0);
                    e.ParrentEntity.SetOnTileOffset(Vector2.Zero);
                    e.Game.World.MoveEntity(e.CurrentLocation, DestinationLocation);
                    e.ParrentEntity.Tags.SetTag("ai_action", -1);
                    GameComponentManager.GetGameObject<ITile>(e.ParrentTile.ID).OnEntityWalkIn(e, gametime);
                }
                else
                {
                    e.ParrentEntity.SetOnTileOffset(e.ParrentEntity.Tags.GetTag("facing",Facing.Down).ToVector2(e.ParrentEntity.Tags.GetTag("move_progress", 0)));
                }

                if (e.ParrentEntity.IsCameraFocus)
                {
                    e.Game.World.Camera.FocusLocation = e.ParrentEntity.Location;
                    e.Game.Camera.PreciseFocusLocation = e.ParrentEntity.GetOnTileOffset();
                }
            }
        }
    }
}
