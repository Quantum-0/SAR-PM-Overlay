using System;
using System.Threading.Tasks;
using System.Windows;

namespace SAR_Overlay
{
    public partial class SARFacade
    {
        public const string WINDOW_NAME = "Super Animal Royale";
        IntPtr hWnd;
        const int delayAfterRefocusToSARWindow = 30;
        const int delayForChatOpening = 20;
        const int maxChatMessageLength = 70;

        private SARFacade(IntPtr window_handle)
        {
            hWnd = window_handle;
        }

        public static SARFacade? CreateFacade()
        {
            var h = NativeMethods.FindWindow(null, WINDOW_NAME);
            if (h.ToInt32() != 0)
                return new SARFacade(h);
            else
                return null;
        }

        private void ExecuteScenarioAction(SARScenario.ScenarioAction sa)
        {
            if (sa is SARScenario.ScenarioActionChatMessage)
                ChatInput(((SARScenario.ScenarioActionChatMessage)(sa)).text);
            else if (sa is SARScenario.ScenarioActionDelay)
                Task.Delay(TimeSpan.FromSeconds(((SARScenario.ScenarioActionDelay)(sa)).seconds)).Wait();
            else if (sa is SARScenario.ScenarioActionKeyboardInput)
            {
                NativeMethods.SetForegroundWindow(hWnd);
                System.Windows.Forms.SendKeys.SendWait(((SARScenario.ScenarioActionKeyboardInput)(sa)).keys);
            }
            else if (sa is SARScenario.ScenarioActionStartMatch)
                Start(((SARScenario.ScenarioActionStartMatch)(sa)).bots);
        }

        public void RunScenario(SARScenario scenario)
        {
            new Task(() =>
            {
                ChatInput(new[] { "SARPMO: Start Scenario {\"}" + scenario.Title + "{\"}" });
                foreach (var sa in scenario.Queue)
                {
                    ExecuteScenarioAction(sa);
                    Task.Delay(100).Wait();
                }
            }).Start();
        }

        public Size GetWindowSize()
        {
            NativeMethods.RECT rect;
            NativeMethods.GetWindowRect(hWnd, out rect);
            return new Size(rect.right - rect.left, rect.bottom - rect.top);
        }

        public bool SetFocusOnGameWindows()
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }
    }
}
