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

        public FormTeleport(SARLocation[] locations)
        {
            InitializeComponent();
            if (locations == null)
                splitContainer1.Panel1Collapsed = true;
            else
            {
                listBoxLocations.Items.Clear();
                foreach (var loc in locations)
                    listBoxLocations.Items.Add(loc);
            }
        }

        private void ListBoxLocations_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedItem == null)
                return;

            selectedCoords = ((SARLocation)(listBoxLocations.SelectedItem)).Coords;
            Close();
        }

        private void FormTeleport_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void PictureBoxMap_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            var coords = new Point((int)(me.Location.X * 4600 / pictureBoxMap.Size.Width), (int)(4600 - (4600 * me.Location.Y / pictureBoxMap.Size.Height)));
            selectedCoords = coords;
            Close();
        }
    }
}
