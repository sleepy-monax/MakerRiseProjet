using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.EngineDebug;
using System;
using System.Windows.Forms;

namespace Maker.RiseEngine.Windows
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var game = new Core.RiseEngine())
            {

                game.Run();

            }

            
        }


    }
#endif
}
