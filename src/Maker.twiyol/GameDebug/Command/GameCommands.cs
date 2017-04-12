using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.RiseEngine.Core.EngineDebug.EngineConsole.Commands;

namespace Maker.twiyol.GameDebug.Command
{
    public static class GameCommands
    {

        public static void AddCommands() {


            CustomCommand twiyol_ver = new CustomCommand("twiyol", a => {
                return "Type ";
            }, "","");

        }

    }
}
