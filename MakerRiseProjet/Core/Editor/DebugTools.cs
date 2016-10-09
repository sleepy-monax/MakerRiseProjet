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
    public partial class DebugTools : Form
    {
        public DebugTools()
        {
            InitializeComponent();
        }

        private void DebugTools_Load(object sender, EventArgs e)
        {



            foreach (string i in GameObjectsManager.GetGameObjList()) {

                listBox1.Items.Add(i);

            }

            tabPage1.VerticalScroll.Value = tabPage1.VerticalScroll.Maximum;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            

        }
    }
}
