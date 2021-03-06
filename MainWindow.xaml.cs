using System;
using System.Collections.Generic;
using System.IO;
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
        FormSpawnItem FSI = new FormSpawnItem();
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

#pragma warning disable CS8601
            SAR = SARFacade.CreateFacade();
#pragma warning restore CS8601

            IntPtr handle = NativeMethods.FindWindow(null, WINDOW_NAME);
            if (SAR == null)
            {
                MessageBox.Show("Cannot find game window", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            SAR.ScenarioPaused += SAR_ScenarioPaused;

            SAR.SetFocusOnGameWindows();

            NativeMethods.RECT rect;
            NativeMethods.GetWindowRect(handle, out rect);
            var windowSize = SAR.GetWindowSize();
            this.Width = windowSize.Width;
            this.Top = windowSize.Height - this.Height;
            this.Left = 0;

            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
            dispatcherTimer.Start();

            ButtonKill.Visibility = Visibility.Collapsed;
            ButtonSpawn.Visibility = Visibility.Collapsed;
        }

        private void SAR_ScenarioPaused(object? sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() => {
                ButtonScenario.Click -= ButtonScenario_Click;
                ButtonScenario.Click += ButtonScenarioResume_Click;
                ButtonScenario.Visibility = Visibility.Visible;
                ButtonSite.Visibility = Visibility.Collapsed;
                ButtonScenario.Content = "Continue";
            });
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => SAR.SetFocusOnGameWindows();

        private void ButtonClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void ButtonMatchID_Click(object sender, RoutedEventArgs e) => SAR.ChatInput("/matchid");

        // Wolfram: atan(pi-x/30) / pi + 0.6; x from 0 to 500; y from 0 to 1
        private void dispatcherTimer_Tick(object sender, EventArgs e) =>
            this.Opacity = Math.Atan(Math.PI - Math.Max(1, this.Top - NativeMethods.GetMousePosition().Y) / 30) / Math.PI + 0.6;

        private void ButtonNight_Click(object sender, RoutedEventArgs e) => SAR.Night();

        private void ButtonGas_Click(object sender, RoutedEventArgs e) => SAR.GasOn = !SAR.GasOn;

        private void ButtonSoccer_Click(object sender, RoutedEventArgs e) => SAR.Soccer();

        private void ButtonTeleport_Click(object sender, RoutedEventArgs e) => new FormTeleport(SAR).ShowDialog();

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (SAR.Start(CheckBoxAddBots.IsChecked))
            {
                ButtonStart.Visibility = Visibility.Collapsed;
                ButtonSoccer.Visibility = Visibility.Collapsed;
                CheckBoxAddBots.Visibility = Visibility.Collapsed;
                ButtonScenario.Visibility = Visibility.Collapsed;
                SliderGasSpeed.Visibility = Visibility.Collapsed;
                ButtonFlight.Visibility = Visibility.Collapsed;

                ButtonSite.Visibility = Visibility.Visible;
                ButtonReload.Visibility = Visibility.Visible;
                ButtonKill.Visibility = Visibility.Visible;
                ButtonSpawn.Visibility = Visibility.Visible;
            }
        }

        private void ButtonScenarioResume_Click(object sender, RoutedEventArgs e)
        {
            ButtonScenario.Click -= ButtonScenarioResume_Click;
            ButtonScenario.Click += ButtonScenario_Click;
            SAR.ResumeScenario();
            ButtonScenario.Content = "Scenario";
            ButtonScenario.Visibility = Visibility.Collapsed;
            ButtonSite.Visibility = Visibility.Visible;
        }

        private void ButtonScenario_Click(object sender, RoutedEventArgs e)
        {
            var frm = new FormSelectScenario(SAR);
            frm.ShowDialog();
            var scenario = frm.Scenario;
            if (scenario != null)
            {
                ButtonScenario.Visibility = Visibility.Collapsed;
                SAR.RunScenario(scenario);

                ButtonStart.Visibility = Visibility.Collapsed;
                ButtonSoccer.Visibility = Visibility.Collapsed;
                CheckBoxAddBots.Visibility = Visibility.Collapsed;
                ButtonScenario.Visibility = Visibility.Collapsed;
                SliderGasSpeed.Visibility = Visibility.Collapsed;
                ButtonFlight.Visibility = Visibility.Collapsed;

                ButtonSite.Visibility = Visibility.Visible;
                ButtonReload.Visibility = Visibility.Visible;
                ButtonKill.Visibility = Visibility.Visible;
                ButtonSpawn.Visibility = Visibility.Visible;
            }
        }

        private void SliderGasSpeed_DragCompleted(object sender, EventArgs e)
        {
            SAR.GasSpeed = (float)SliderGasSpeed.Value;
            SliderGasSpeed.Value = SAR.GasSpeed;
        }

        private void SliderGasDamage_DragCompleted(object sender, EventArgs e)
        {
            SAR.GasDamage = (float)SliderGasDamage.Value;
            SliderGasDamage.Value = SAR.GasDamage;
        }

        private void ButtonSpawn_Click(object sender, RoutedEventArgs e)
        {
            FSI.ShowDialog();
            if (FSI.WasClosed)
                FSI = new FormSpawnItem();
            else if (!string.IsNullOrEmpty(FSI.Command))
                SAR.ChatInput(FSI.Command);
        }

        private void ButtonSpawn_MouseRightButtonDown(object sender, MouseButtonEventArgs e) => SAR.ChatInput(FSI.Command);

        private void ButtonOneHits_Click(object sender, RoutedEventArgs e) => SAR.OneHits = !SAR.OneHits;

        private void ButtonFlight_Click(object sender, RoutedEventArgs e) => SAR.Flight();

        private void ButtonKill_Click(object sender, RoutedEventArgs e) => new FormSelectPlayer(SAR, SAR.Kill).ShowDialog();

        private void ButtonKick_Click(object sender, RoutedEventArgs e) => new FormSelectPlayer(SAR, SAR.Kick).ShowDialog();

        private void ButtonInfect_Click(object sender, RoutedEventArgs e) => new FormSelectPlayer(SAR, SAR.Infect).ShowDialog();

        private void ButtonGod_Click(object sender, RoutedEventArgs e) => new FormSelectPlayer(SAR, SAR.God).ShowDialog();

        private void ButtonGhost_Click(object sender, RoutedEventArgs e) => new FormSelectPlayer(SAR, SAR.Ghost).ShowDialog();

        private void ButtonGod_MouseRightButtonDown(object sender, MouseButtonEventArgs e) => SAR.God(SAR.Me);

        private void ButtonGhost_MouseRightButtonDown(object sender, MouseButtonEventArgs e) => SAR.Ghost(SAR.Me);

        private void ButtonKill_MouseRightButtonDown(object sender, MouseButtonEventArgs e) => SAR.Kill(SAR.Me);

        private void ButtonSite_Click(object sender, RoutedEventArgs e) =>
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "http://sarpmo.quantum0.ru", UseShellExecute = true });

        private void ButtonReload_Click(object sender, RoutedEventArgs e)
        {
            SAR = SARFacade.CreateFacade();

            ButtonScenario.Visibility = Visibility.Visible;
            ButtonStart.Visibility = Visibility.Visible;
            ButtonSoccer.Visibility = Visibility.Visible;
            CheckBoxAddBots.Visibility = Visibility.Visible;
            ButtonScenario.Visibility = Visibility.Visible;
            SliderGasSpeed.Visibility = Visibility.Visible;
            ButtonFlight.Visibility = Visibility.Visible;

            ButtonSite.Visibility = Visibility.Collapsed;
            ButtonReload.Visibility = Visibility.Collapsed;
            ButtonKill.Visibility = Visibility.Collapsed;
            ButtonSpawn.Visibility = Visibility.Collapsed;
            SliderGasSpeed.Value = 1;
            SliderGasDamage.Value = 1;
            CheckBoxAddBots.IsChecked = false;
        }
    }
}
