using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RiseEngine.Core.World.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.GameObject.Event
{
    public class GameObjectEventArgs
    {

        public World.Obj.ObjEntity ParrentEntity;
        public World.Obj.ObjTile ParrentTile;

        public WorldLocation CurrentLocation;
        public World.WorldScene World;

        public Point OnScreenLocation;

    }
}
