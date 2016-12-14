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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string file;
        Core.Config.EngineConfig c;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();


            if (od.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    file = od.FileNames[0];
                    c    = Core.Storage.SerializationHelper.LoadFromBin<Core.Config.EngineConfig>(file);

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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Core.Storage.SerializationHelper.SaveToBin(c, file);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
