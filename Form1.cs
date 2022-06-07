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

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!NativeMethods.IsAdministrator)
                label1.Visible = true;
            else
                this.Size = new Size(this.Size.Width, this.Size.Height - 35);
        }

        private void ButtonStartOverlay_Click(object sender, EventArgs e)
        {
            if (!NativeMethods.IsAdministrator)
            {
                MessageBox.Show("The program cannot emulate user input without admin rights", "No admin rights", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sar = SARFacade.CreateFacade();
            if (sar == null)
            {
                MessageBox.Show("Cannot find game window", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fm.SAR = sar;
            fm.Show();
            sar.SetFocusOnGameWindows();
        }
    }
}
