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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAR_Overlay
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SARFacade SAR;
        public const string WINDOW_NAME = "Super Animal Royale";
        // TODO: Night: Green/Red, Gas: Green/Red

        public MainWindow()
        {
            InitializeComponent();
            if (!NativeMethods.IsAdministrator)
            {
                MessageBox.Show("The program cannot emulate user input without admin rights", "No admin rights", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            SAR = SARFacade.CreateFacade();

            IntPtr handle = NativeMethods.FindWindow(null, WINDOW_NAME);
            if (SAR == null)
            {
                MessageBox.Show("Cannot find game window", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            SAR.SetFocusOnGameWindows();

            NativeMethods.RECT rect;
            NativeMethods.GetWindowRect(handle, out rect);
            var windowSize = SAR.GetWindowSize();
            this.Width = windowSize.Width;
            this.Top = windowSize.Height - this.Height;
            this.Left = 0;

            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,0,0,25);
            dispatcherTimer.Start();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => SAR.SetFocusOnGameWindows();

        private void ButtonClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void ButtonMatchID_Click(object sender, RoutedEventArgs e) => SAR.ChatInput("/matchid");

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Wolfram: atan(pi-x/30) / pi + 0.6; x from 0 to 500; y from 0 to 1
            this.Opacity = Math.Atan(Math.PI - Math.Max(1, this.Top - NativeMethods.GetMousePosition().Y) / 30) / Math.PI + 0.6;
        }

        private void ButtonNight_Click(object sender, RoutedEventArgs e) => SAR.SwitchNight();

        private void ButtonGas_Click(object sender, RoutedEventArgs e) => SAR.GasOn = !SAR.GasOn;

        private void ButtonSoccer_Click(object sender, RoutedEventArgs e) => SAR.Soccer();

        private void ButtonTeleport_Click(object sender, RoutedEventArgs e)
        {
            new FormTeleport(SAR).ShowDialog();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (SAR.Start(CheckBoxAddBots.IsChecked))
            {
                ButtonStart.Visibility = Visibility.Collapsed;
                ButtonSoccer.Visibility = Visibility.Collapsed;
                CheckBoxAddBots.Visibility = Visibility.Collapsed;
                ButtonScenario.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonScenario_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
