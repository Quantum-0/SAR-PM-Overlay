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
    /// Логика взаимодействия для FormTeamSelect.xaml
    /// </summary>
    public partial class FormTeamSelect : Window
    {
        public List<SARPlayer> Team1 = new List<SARPlayer>();
        public List<SARPlayer> Team2 = new List<SARPlayer>();
        public List<SARPlayer> NoTeam = new List<SARPlayer>();

        public FormTeamSelect(IEnumerable<SARPlayer> players)
        {
            InitializeComponent();
            foreach (var pl in players)
                ListBoxPlayers.Items.Add(pl.WithTeam(0));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (SARPlayerWithTeam pl in ListBoxPlayers.Items)
            {
                if (pl.Team1)
                    Team1.Add(pl);
                else if (pl.Team2)
                    Team2.Add(pl);
                else
                    NoTeam.Add(pl);
            }
        }
    }
}
