using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace SAR_Overlay
{
    public partial class FormOverlay : Form
    {
        public const string WINDOW_NAME = "Super Animal Royale";
        IntPtr handle = NativeMethods.FindWindow(null, WINDOW_NAME);

        public FormOverlay()
        {
            InitializeComponent();
        }

        private void FormOverlay_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;

            int initialStyle = NativeMethods.GetWindowLongPtr(this.Handle, -20).ToInt32();
            //SetWindowLongPtr(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            NativeMethods.RECT rect;
            NativeMethods.GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Size = new Size(this.Size.Width, 48);
            this.Top = rect.bottom - 48;
            this.Left = rect.left;
        }

        

        private void FormOverlay_Activated(object sender, EventArgs e)
        {

            if (NativeMethods.SetForegroundWindow(handle))
            {
                Task.Delay(50).Wait();
                SendKeys.SendWait("{ENTER}/night{ENTER}");
            }
        }

        private void FormOverlay_Enter(object sender, EventArgs e)
        {
        }
    }
}
