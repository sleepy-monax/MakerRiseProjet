using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.World.Utils
{
    public class CommandParse
    {
        WorldScene W;

        public CommandParse(WorldScene _WorldScene)
        {
            W = _WorldScene;
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
