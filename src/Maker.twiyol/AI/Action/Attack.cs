using Maker.RiseEngine.GameObjects;
using Maker.Twiyol.AI;
using Maker.Twiyol.Game.GameUtils;
using Maker.Twiyol.Game.WorldDataStruct;
using Maker.Twiyol.GameObject;
using Maker.Twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using System;
using static Maker.Twiyol.AI.Utils;

namespace Maker.Twiyol.AI.Action
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
            DataEntity attackerEntity = e.ParrentEntity;
            Point targetLocation = e.CurrentLocation.ToPoint() + e.ParrentEntity.Tags.GetTag("facing", Facing.Down).ToPoint();

            // Checking if there are a entity on target location and if the attack cool down.
            if (!(e.Game.World.IsEntityFree(targetLocation.ToWorldLocation())) && attackerEntity.Tags.GetTag<int>("attack_cooldown", 0) == 0)
            {
                // Get attacker damages.
                float attackerDamages = e.ParrentEntity.ToGameObject().GetDamages(e);

                // Get target life and defense
                DataEntity targetEntity = e.Game.World.GetEntity(targetLocation);
                GameObjectEventArgs targetEntityEventsArgs = e.Game.eventsManager.GetEventArgs(targetLocation.ToWorldLocation(), e.OnScreenLocation);
                int targetHeal = targetEntity.Tags.GetTag("heal", targetEntity.ToGameObject().MaxHeal);
                float targetDefense = targetEntity.ToGameObject().GetDefence(targetEntityEventsArgs);

                // Calculate attack points.
                float totalDamages = attackerDamages - targetDefense;

                totalDamages = attackerDamages < 0 ? 0 : totalDamages;

                targetHeal -= (int)totalDamages;
                targetEntity.Tags.SetTag("heal", targetHeal);
                targetEntity.ToGameObject().OnDamagesTaken(targetEntityEventsArgs);

                // Kill the entity if heal is under 0.
                if (targetHeal <= 0) {
                    targetEntity.ToGameObject().OnEntityKilled(targetEntityEventsArgs, attackerEntity);
                }

                attackerEntity.Tags.SetTag("attack_cooldown", 30);
               
            }

            // Remove the action.
            attackerEntity.Tags.SetTag("ai_action", -1);

        }
    }
}
