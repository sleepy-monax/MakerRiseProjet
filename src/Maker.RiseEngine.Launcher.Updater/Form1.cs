using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maker.RiseEngine.Launcher.Updater
{
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
        }

        public void DownloadFile(string fileURL,string filePath) {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
