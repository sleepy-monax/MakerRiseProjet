using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RiseEngine.Core.GameObject.Event;
using RiseEngine.Core.World.Utils;
using RiseEngine.Core.World.WorldObj;

namespace RiseEngine.Core.AI.Action
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

                ObjEntity attackedEntity = e.World.chunkManager.GetEntity(CurrentLocation);
                GameObjectEventArgs attackedEntityEventsArgs = e.World.eventsManager.GetEventArgs(CurrentLocation.ToWorldLocation(), e.OnScreenLocation);

                float defense = attackedEntity.ToGameObject().GetDamage(attackedEntityEventsArgs);
                float damages = e.ParrentEntity.ToGameObject().GetDamage(e);

                
                float totalDamages = damages - defense;
                if (totalDamages < 0)
                    totalDamages = 0;

            }

        }
    }
}
