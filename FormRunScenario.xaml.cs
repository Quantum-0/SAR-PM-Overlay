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
    /// Логика взаимодействия для FormRunScenario.xaml
    /// </summary>
    public partial class FormRunScenario : Window
    {
        private SARFacade SAR;

        public FormRunScenario(SARFacade SAR)
        {
            InitializeComponent();
            this.SAR = SAR;
        }
    }
}
