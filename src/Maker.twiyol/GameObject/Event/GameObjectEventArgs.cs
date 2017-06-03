using Maker.Twiyol.Game;
using Maker.Twiyol.Game.GameUtils;
using Maker.Twiyol.Game.WorldDataStruct;
using Microsoft.Xna.Framework;

namespace Maker.Twiyol.GameObject.Event
{
    public class GameObjectEventArgs
    {

        public DataEntity ParrentEntity;
        public DataTile ParrentTile;

        public WorldLocation CurrentLocation;
        public GameScene Game;

        public Point OnScreenLocation;

    }
}
