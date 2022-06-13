using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAR_Overlay
{
    /// <summary>
    /// Логика взаимодействия для FormTeleport.xaml
    /// </summary>
    public partial class FormTeleport : Window
    {
        SARFacade SAR;

        private void TeleportAndClose(Point location)
        {
            if ((ListBoxPlayerSelect.SelectedItem as ListBoxItem).Content == "Me")
                SAR.Teleport(location);
            else
                SAR.Teleport(location, ((ListBoxPlayerSelect.SelectedItem as ListBoxItem).Content as SARPlayer).pID);
            Close();
        }

        public FormTeleport(SARFacade SAR)
        {
            InitializeComponent();

            this.SAR = SAR;

            foreach (var location in Config.TeleportLocations)
            {
                var loc = new ListBoxItem() { Content = location };
                ListBoxPredefinedLocations.Items.Add(loc);
            }

            foreach (var player in SAR.GetPlayers())
            {
                var pl = new ListBoxItem() { Content = player, FontStyle = player.isBot ? FontStyles.Italic : FontStyles.Normal };
                ListBoxPlayerSelect.Items.Add(pl);
            }
        }

        private void ImageMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = Mouse.GetPosition(ImageMap);
            var coords = new Point((int)(mousePos.X * 4600 / ImageMap.ActualWidth), (int)(4600 - (4600 * mousePos.Y / ImageMap.ActualHeight)));
            TeleportAndClose(coords);
        }

        private void ListBoxPredefinedLocations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListBoxPredefinedLocations.SelectedItem != null)
                TeleportAndClose(((ListBoxPredefinedLocations.SelectedItem as ListBoxItem).Content as SARLocation).Coords);
        }
    }
}
