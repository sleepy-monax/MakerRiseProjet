using Maker.Twiyol.GameObject.Event;
using Microsoft.Xna.Framework;

namespace Maker.Twiyol.Game.GameUtils
{
    public class EventsManager
    {
        GameScene G;
        public EventsManager(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        public GameObjectEventArgs GetEventArgs(WorldLocation location, Point onScreenLocation)
        {
            GameObjectEventArgs args = new GameObjectEventArgs()
            {
                CurrentLocation = location,
                ParrentTile = G.World.GetTile(location),
                Game = G,
                OnScreenLocation = onScreenLocation
            };

            if (args.ParrentTile.Entity == -1)
                args.ParrentEntity = new WorldDataStruct.DataEntity( -1 , -1);
            else
                args.ParrentEntity = G.World.GetEntity(location);

            return args;
        }
    }
}
