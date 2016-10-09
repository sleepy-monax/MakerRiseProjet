using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiseEngine.Core.Editor
{
    public partial class OpenPlugin : Form
    {
        public OpenPlugin()
        {
            InitializeComponent();
        }

        private void OpenPlugin_Load(object sender, EventArgs e)
        {
            string[] Dirs = System.IO.Directory.GetDirectories("Data");
            foreach (string Dir in Dirs) {

                string[] SubDir = Dir.Split('\\');

                listBox1.Items.Add(SubDir[SubDir.Count() - 1]);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Plugin.PluginEditor PLE = new Plugin.PluginEditor(listBox1.Text);
            PLE.Show();
            this.Close();
        }
    }
}
