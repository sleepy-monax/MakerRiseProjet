using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;

namespace Maker.twiyol.Game.GameUtils
{
    public class EventsManager
    {
        GameScene G;
        public EventsManager(GameScene _WorldScene)
        {
            G = _WorldScene;
        }


        public GameObjectEventArgs GetEventArgs(WorldLocation Location, Point _OnScreenLocation)
        {

            GameObject.Event.GameObjectEventArgs args = new GameObject.Event.GameObjectEventArgs();

            args.CurrentLocation = Location;

            args.ParrentTile = G.chunkManager.GetTile(Location);

            if (args.ParrentTile.Entity == -1) {

                args.ParrentEntity = new WorldDataStruct.DataEntity( -1 , -1);

            } else {

            args.ParrentEntity = G.chunkManager.GetEntity(Location);

            }

            args.World = G;
            args.OnScreenLocation = _OnScreenLocation;

            return args;

        }
    }
}
