using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private SARLocation[] Locations;

        public SARFacade SAR;

        public OverlayForm()
        {
            InitializeComponent();

            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void LoadLocations()
        {
            if (Directory.Exists("../../Config"))
            {
                if (File.Exists("../../Config/Locations.txt"))
                {
                    var locations = File.ReadAllText("../../Config/Locations.txt");
                    Locations = locations.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(loc => SARLocation.Parse(loc)).ToArray();
                }
            }
            else if (Directory.Exists("Config"))
            {
                if (File.Exists("Config/Locations.txt"))
                {
                    var locations = File.ReadAllText("Config/Locations.txt");
                    Locations = locations.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(loc => SARLocation.Parse(loc)).ToArray();
                }
            }
        }

        private void ButtonMatchID_Click(object sender, EventArgs e)
        {
            SAR.ChatInput("/matchid");
        }

        private void ButtonNight_Click(object sender, EventArgs e)
        {
            SAR.ChatInput("/night");
        }

        private void ButtonSoccer_Click(object sender, EventArgs e)
        {
            SAR.ChatInput("/soccer");
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
            if (SAR.Start(checkBox1.Checked))
            {
                checkBox1.Visible = false;
                buttonStart.Visible = false;
                buttonSoccer.Visible = false;
            }
        }

        private void ButtonTeleport_Click(object sender, EventArgs e)
        {
            var windowSize = SAR.GetWindowSize();
            var prevPost = Cursor.Position;
            Cursor.Position = new Point(windowSize.Width / 2, windowSize.Height / 2);
            LoadLocations();
            var f = new FormTeleport(Locations);
            f.ShowDialog();
            Cursor.Position = prevPost;
            SAR.SetFocusOnGameWindows();
            SAR.Teleport(f.selectedCoords, 1);
        }

        private void OverlayForm_Click(object sender, EventArgs e)
        {
            SAR.SetFocusOnGameWindows();
        }

        private void ButtonSwithGas_Click(object sender, EventArgs e)
        {
            SAR.GasOn = !SAR.GasOn;
        }

        private void ButtonDuel_Click(object sender, EventArgs e)
        {
            // TODO: Open dialog with choosing scenarious from folder
            var sce = File.ReadAllText("../../Duel.sarpms");
            SAR.RunScenario(Scenario.Parse(sce));
        }

        private void TrackBarGasSpeed_Scroll(object sender, EventArgs e)
        {
            SAR.GasSpeed = (float)(trackBarGasSpeed.Value) / 10;
        }

        private void TrackBarGasDamage_Scroll(object sender, EventArgs e)
        {
            SAR.GasDamage = (float)(trackBarGasDamage.Value) / 10;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
    }
}