using Maker.Twiyol.Game.GameUtils;
using Microsoft.Xna.Framework;

namespace Maker.Twiyol.GameObject.Event
{
    public class GameObjectEventArgs
    {

        public Game.WorldDataStruct.DataEntity ParrentEntity;
        public Game.WorldDataStruct.DataTile ParrentTile;

        public WorldLocation CurrentLocation;
        public Game.GameScene Game;

        public Point OnScreenLocation;

    }
}
