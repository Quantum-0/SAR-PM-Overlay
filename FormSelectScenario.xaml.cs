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
    public partial class FormSelectScenario : Window
    {
        private SARFacade SAR;
        public SARScenario Scenario;

        public FormSelectScenario(SARFacade sar)
        {
            InitializeComponent();
            SAR = sar;

            foreach (var scenario in Config.ScenariosList)
                ListBoxScenarioSelect.Items.Add(new ListBoxItem() { Content = scenario });
        }

        private void ListBoxScenarioSelect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListBoxScenarioSelect.SelectedItem != null && ((ListBoxItem)(ListBoxScenarioSelect.SelectedItem)).Content is SARScenario)
            {
                //SAR.RunScenario((SARScenario)((ListBoxItem)(ListBoxScenarioSelect.SelectedItem)).Content);
                Scenario = (SARScenario)((ListBoxItem)(ListBoxScenarioSelect.SelectedItem)).Content;
                Close();
            }
        }

        private void TextBoxFilter_GotFocus(object sender, EventArgs e)
        {
            TextBoxFilter.SelectAll();
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var lbi in ListBoxScenarioSelect.Items)
            {
                #pragma warning disable CS8602
                ((ListBoxItem)lbi).Visibility = ((ListBoxItem)lbi).Content.ToString().Contains(TextBoxFilter.Text) ? Visibility.Visible : Visibility.Collapsed;
                #pragma warning restore CS8602
            }
        }
    }
}
