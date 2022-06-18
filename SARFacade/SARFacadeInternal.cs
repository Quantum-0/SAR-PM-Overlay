﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
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
        public string? SteamUsername { get; private set; } = null;
        private bool ScenarioPause = false;

        public event EventHandler<EventArgs> ScenarioPaused;

        private SARFacade(IntPtr window_handle)
        {
            hWnd = window_handle;
            this.SteamUsername = TryReadSteamUsername();
        }

        public static SARFacade? CreateFacade()
        {
            var h = NativeMethods.FindWindow(null, WINDOW_NAME);
            if (h.ToInt32() != 0)
                return new SARFacade(h);
            else
                return null;
        }

        private static string? TryReadSteamUsername()
        {
            // "\d+"\s*{[\s\S]*?"PersonaName"\t\t"(.*)"\n[\s\S]*?"mostrecent"\t\t"1"[^}]*}
            const string regexppattern = "\"\\d+\"\\s*{[\\s\\S]*?\"PersonaName\"\\t\\t\"(.*)\"\\n[\\s\\S]*?\"mostrecent\"\\t\\t\"1\"[^}]*}";
            var steamFolder = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "InstallPath", null);
            if (steamFolder == null)
                steamFolder = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", "InstallPath", null);
            if (steamFolder == null)
                return null;
            var loginUsersFname = Path.Combine((string)steamFolder, @"config\loginusers.vdf");
            if (!File.Exists(loginUsersFname))
                return null;
            string content;
            try
            {
                content = File.ReadAllText(loginUsersFname);
            }
            catch
            {
                return null;
            }
            var match = Regex.Match(content, regexppattern);
            if (!match.Success)
                return null;
            return match.Groups[1].Value;
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
            else if (sa is SARScenario.ScenarioActionPause)
            {
                ScenarioPause = true;
                ScenarioPaused.Invoke(this, EventArgs.Empty);
            }
        }

        public void ResumeScenario() => ScenarioPause = false;

        public void RunScenario(SARScenario scenario)
        {
            new Task(() =>
            {
                ChatInput(new[] { "SARPMO: Start Scenario {\"}" + scenario.Title + "{\"}" });
                foreach (var sa in scenario.Queue)
                {
                    ExecuteScenarioAction(sa);
                    do
                    {
                        Task.Delay(100).Wait();
                    }
                    while (ScenarioPause);
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
