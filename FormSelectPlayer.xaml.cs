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
    /// Логика взаимодействия для FormSelectPlayer.xaml
    /// </summary>
    public partial class FormSelectPlayer : Window
    {
        private SARFacade SAR;
        private Func<SARPlayer, bool> Action;

        public FormSelectPlayer(SARFacade sar, Func<SARPlayer, bool> action)
        {
            InitializeComponent();
            SAR = sar;
            Action = action;
            var players = SAR.GetPlayers();
            if (players != null)
                foreach (var player in players)
                {
                    var pl = new ListBoxItem() { Content = player, FontStyle = player.IsBot ? FontStyles.Italic : FontStyles.Normal };
                    ListBoxPlayerSelect.Items.Add(pl);
                }
        }

        private void ListBoxPlayerSelect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListBoxPlayerSelect.SelectedItem != null && ((ListBoxItem)(ListBoxPlayerSelect.SelectedItem)).Content is SARPlayer)
            {
                Action.Invoke((SARPlayer)((ListBoxItem)(ListBoxPlayerSelect.SelectedItem)).Content);
                Close();
            }
        }
    }
}
