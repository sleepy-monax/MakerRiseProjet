using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.World.Utils
{
    public class EventsManager
    {
        WorldScene W;
        public EventsManager(WorldScene _WorldScene)
        {
            W = _WorldScene;
        }


        public GameObject.Event.GameObjectEventArgs GetEventArgs(WorldLocation Location, Point _OnScreenLocation)
        {

            GameObject.Event.GameObjectEventArgs args = new GameObject.Event.GameObjectEventArgs();

            args.CurrentLocation = Location;

            args.ParrentTile = W.chunkManager.GetTile(Location);

            if (args.ParrentTile.Entity == -1) {

                args.ParrentEntity = new WorldObj.ObjEntity( -1 , -1);

            } else {

            args.ParrentEntity = W.chunkManager.GetEntity(Location);

            }

            args.World = W;
            args.OnScreenLocation = _OnScreenLocation;

            return args;

        }
    }
}
