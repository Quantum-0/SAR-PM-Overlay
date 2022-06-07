using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAR_Overlay
{
    public partial class MainForm : Form
    {
        OverlayForm fm = new OverlayForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                fm.Show();
            }
            else
            {
                fm.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!NativeMethods.IsAdministrator)
                label1.Visible = true;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
