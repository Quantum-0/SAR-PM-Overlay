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
    public partial class OverlayForm : Form
    {
        public const string WINDOW_NAME = "Super Animal Royale";
        IntPtr handle = NativeMethods.FindWindow(null, WINDOW_NAME);

        public OverlayForm()
        {
            InitializeComponent();

            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void ButtonMatchID_Click(object sender, EventArgs e)
        {
            if (NativeMethods.SetForegroundWindow(handle))
                SendKeys.SendWait("{ENTER}/matchid{ENTER}");
        }

        private void ButtonNight_Click(object sender, EventArgs e)
        {
            if (NativeMethods.SetForegroundWindow(handle))
                SendKeys.SendWait("{ENTER}/night{ENTER}");
        }

        private void ButtonSoccer_Click(object sender, EventArgs e)
        {
            if (NativeMethods.SetForegroundWindow(handle))
                SendKeys.SendWait("{ENTER}/soccer{ENTER}");
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            NativeMethods.RECT rect;
            NativeMethods.GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Size = new Size(this.Size.Width, 48);
            this.Top = rect.bottom - 48;
            this.Left = rect.left;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            /*
            var handler_start_p = new EventHandler(delegate (object s, EventArgs ev)
            {
                if (NativeMethods.SetForegroundWindow(handle))
                    SendKeys.SendWait("{ENTER}/startp{ENTER}");
            });

            var handler_start = new EventHandler(delegate (object s, EventArgs ev)
            {
                if (NativeMethods.SetForegroundWindow(handle))
                    SendKeys.SendWait("{ENTER}/start{ENTER}");
            });

            var startMenu = new ContextMenu();
            startMenu.MenuItems.Add("С Ботами", handler_start);
            startMenu.MenuItems.Add("Без ботов", handler_start_p);
            startMenu.Show(sender as Control, MousePosition);
            */
        }
    }
}
