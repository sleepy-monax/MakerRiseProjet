using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakerRiseEngine.Windows.NoCMD
{
    class Program
    {
        [STAThread]
        static void Main()
        {

            using (var game = new RiseEngine.RiseGame(false))
                game.Run();
        }
    }
}
