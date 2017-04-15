using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maker.RiseEngine.ConfigEditor
{
    public partial class MainFrm : Form
    {
        string file;
        object c;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();


            if (od.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    file = od.FileNames[0];
                    c = Core.Storage.SerializationHelper.LoadFromBin<Object>(file);

                    propertyGrid1.Enabled = true;
                    button2.Enabled = true;

                    propertyGrid1.SelectedObject = c;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
        }
    }
}
