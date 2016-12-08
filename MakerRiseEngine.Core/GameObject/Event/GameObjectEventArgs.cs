using Maker.RiseEngine.Core.Game.GameUtils;
using Microsoft.Xna.Framework;

namespace Maker.RiseEngine.Core.GameObject.Event
{
    public class GameObjectEventArgs
    {

        public Game.World.ObjEntity ParrentEntity;
        public Game.World.ObjTile ParrentTile;

        public WorldLocation CurrentLocation;
        public Game.GameScene World;

        public Point OnScreenLocation;

    }
}
