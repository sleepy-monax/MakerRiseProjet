
using Maker.RiseEngine.Core.Game.GameUtils;
using Maker.RiseEngine.Core.Game.World;
using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.GameObject.Event;
using Microsoft.Xna.Framework;

namespace Maker.RiseEngine.Core.AI.Action
{
    public class Attack : IAction
    {
        public string gameObjectName { get; set; }
        public string pluginName { get; set; }

        public void OnGameObjectAdded()
        {

        }

        public void Performe(GameObjectEventArgs e, GameTime gametime)
        {

            Point CurrentLocation = e.CurrentLocation.ToPoint() + e.ParrentEntity.Facing.ToPoint();

            if (!(e.World.entityManager.TileIsFree(CurrentLocation.ToWorldLocation())))
            {

                e.ParrentEntity.ActionProgress += GameObjectsManager.GetGameObject<IEntity>(e.ParrentEntity.ID).MoveSpeed;

                if (e.ParrentEntity.ActionProgress == 100)
                {
                    e.ParrentEntity.ActionProgress = 0;
                    e.ParrentEntity.OnTileLocation = Vector2.Zero;

                    ObjEntity attackedEntity = e.World.chunkManager.GetEntity(CurrentLocation);
                    GameObjectEventArgs attackedEntityEventsArgs = e.World.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), e.OnScreenLocation);

                    float defense = attackedEntityEventsArgs.ParrentEntity.ToGameObject().GetDefence(attackedEntityEventsArgs);
                    float damages = e.ParrentEntity.ToGameObject().GetDamage(e);


                    float totalDamages = damages - defense;
                    if (totalDamages < 0)
                        totalDamages = 0;

                    attackedEntityEventsArgs.ParrentEntity.heal -= totalDamages;

                    attackedEntityEventsArgs.ParrentEntity.ToGameObject().OnDamageTaken(attackedEntityEventsArgs);

                    if (attackedEntityEventsArgs.ParrentEntity.heal <= 0)
                    {
                        attackedEntityEventsArgs.ParrentEntity.heal = 0;
                        attackedEntityEventsArgs.ParrentEntity.ToGameObject().OnEntityKilled(attackedEntityEventsArgs, e.ParrentEntity);
                    }

                    e.ParrentEntity.Action = -1;
                    e.ParrentEntity.ActionProgress = 0;

                }
                else
                {
                    e.ParrentEntity.OnTileLocation = e.ParrentEntity.Facing.ToVector2(e.ParrentEntity.ActionProgress);
                }
            }
            else {

                e.ParrentEntity.Action = -1;
                e.ParrentEntity.ActionProgress = 0;

            }

        }
    }
}
