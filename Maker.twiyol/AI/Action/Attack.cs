using Maker.RiseEngine.Core.GameObject;
using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.Game.WorldDataStruct;
using Maker.twiyol.GameObject;
using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;

namespace Maker.twiyol.AI.Action
{
    public class Attack : IAction
    {
        public string GameObjectName { get; set; }
        public string PluginName { get; set; }

        public void OnGameObjectAdded()
        {

        }

        public void Performe(GameObjectEventArgs e, GameTime gametime)
        {

            Point CurrentLocation = e.CurrentLocation.ToPoint() + e.ParrentEntity.Facing.ToPoint();

            if (!(e.Game.World.IsEntityFree(CurrentLocation.ToWorldLocation())))
            {

                e.ParrentEntity.ActionProgress += GameObjectManager.GetGameObject<IEntity>(e.ParrentEntity.ID).MoveSpeed;

                if (e.ParrentEntity.ActionProgress == 100)
                {

                    // Get entityies.
                    DataEntity attackedEntity = e.Game.World.GetEntity(CurrentLocation);
                    GameObjectEventArgs attackedEntityEventsArgs = e.Game.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), e.OnScreenLocation);

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

                    // remove the action.
                    e.ParrentEntity.Action = -1;
                    e.ParrentEntity.ActionProgress = 0;
                    e.ParrentEntity.SetOnTileLocation(new Vector2(0));

                }
                else
                {
                    if (e.ParrentEntity.ActionProgress > 75)
                    {
                        e.ParrentEntity.SetOnTileLocation(e.ParrentEntity.Facing.ToVector2((50 - e.ParrentEntity.ActionProgress) / 2));
                    }
                    else
                    {
                        e.ParrentEntity.SetOnTileLocation(e.ParrentEntity.Facing.ToVector2(e.ParrentEntity.ActionProgress / 2));
                    }

                }
            }
            else
            {

                e.ParrentEntity.Action = -1;
                e.ParrentEntity.ActionProgress = 0;

            }

        }
    }
}
