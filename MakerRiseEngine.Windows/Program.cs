using System;
using System.Windows.Forms;

namespace RiseEngine.Windows
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


            if (System.IO.File.Exists("MakerRiseEngine.dll"))
            {
                using (var game = new RiseEngine.RiseGame(false))
                {

                    game.Run();

                }
            }
            else
            {

                if (MessageBox.Show("Engine Not found ") == DialogResult.OK) { }

            }


        }


    }
#endif
}
