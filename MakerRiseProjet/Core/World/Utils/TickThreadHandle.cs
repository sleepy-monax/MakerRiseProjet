using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RiseEngine.Core.World.Utils
{
    class TickThreadHandle
    {

        WorldScene W;

        public TickThreadHandle(WorldScene _WorldScene)
        {
            W = _WorldScene;
        }

        // Méthode boucle du thread
        public void ThreadLoop()
        {

            while (true)
            {

                Thread.Sleep(100);


            }

        }

    }
}
