using Maker.twiyol;
using Maker.twiyol.Game;
using Maker.twiyol.Game.WorldDataStruct;
using Maker.twiyol.Game.GameUtils;
using Microsoft.Xna.Framework;
using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.GameObject;

namespace Maker.RiseEngine.DefaultPlugin
{
    class GameEventHandler : GameEventHandle
    {
        IPlugin p;
        public GameEventHandler(IPlugin _p) {
            p = _p;
        }

        public override void OnWorldGeneration(GameScene world)
        {
            DataEntity E = new DataEntity(p.GetGameObjectIndex("Player"), 0);
            E.IsFocus = true;

            world.EntityDataManager.RemoveEntityData(new WorldLocation(new Point(5, 5), new Point(5, 5)));
            world.EntityDataManager.AddEntityData(E, new WorldLocation(new Point(5, 5), new Point(5, 5)));
            world.Camera.FocusLocation = new WorldLocation(new Point(5, 5), new Point(5, 5)).ToPoint();
            world.Camera.Update();
        }
    }
}
