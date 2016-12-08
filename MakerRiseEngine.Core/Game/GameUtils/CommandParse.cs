using System;

namespace Maker.RiseEngine.Core.Game.GameUtils
{
    public class CommandParse
    {
        GameScene G;

        public CommandParse(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        public string Parse(String _cmd) {

            string rtrn = "";
            string[] WorkString = _cmd.Split('(');

            string Command = WorkString[0];
            WorkString = WorkString[1].Split(')');
            string[] Args = WorkString[0].Split(',');

            switch (Command)
            {
                case "fill":



                    break;
                case "set":



                    break;


                default:
                    break;
            }

            return rtrn;
        }

    }
}
