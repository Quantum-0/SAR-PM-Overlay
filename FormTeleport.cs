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
    public partial class FormTeleport : Form
    {
        public Point selectedCoords;

        public FormTeleport()
        {
            InitializeComponent();
        }

        private void ListBoxLocations_DoubleClick(object sender, EventArgs e)
        {
            var coords = listBoxLocations.SelectedItem.ToString().Split('-').First().Split(' ').Take(2).Select(coord => int.Parse(coord)).ToArray();
            selectedCoords = new Point(coords[0], coords[1]);
            Close();
        }

        private void FormTeleport_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
