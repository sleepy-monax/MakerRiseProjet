using Maker.twiyol.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.twiyol.Events
{
    public static class GameEventHandler
    {
        public static event EventHandler OnWorldGenerating;
        public static void RaiseOnWorldGenerating(object sender, GameScene world) {
            OnWorldGenerating?.Invoke(sender, new WorldEventArgs(world));
        }

    }
}
