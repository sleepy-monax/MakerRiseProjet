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

            Console.WriteLine("Number Of Logical Processors: {0}", Environment.ProcessorCount);


            using (var game = new Core.RiseGame(false))
            {

                game.Run();

            }
        }


    }
#endif
}
