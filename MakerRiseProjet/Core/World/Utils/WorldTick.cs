using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RiseEngine.Core.World.Utils
{
    public class WorldTick
    {

        WorldScene W;
        Thread T;

        public WorldTick(WorldScene _WorldScene)
        {
            W = _WorldScene;

            TickThreadHandle THandle = new TickThreadHandle(W);

            Thread _T = new Thread(new ThreadStart(THandle.ThreadLoop));
            T = _T;
        }

        public void start() {
            T.Start();
        }


    }
}
