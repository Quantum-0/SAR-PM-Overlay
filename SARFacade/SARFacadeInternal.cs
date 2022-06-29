using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        private Random rnd = new Random();

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

        private string[] ParseScenarioTemplates(string command, SARPlayer[] playerList, List<List<SARPlayer>> teams, int repeatIndex)
        {
            command = command.Replace("<ME>", (this.Me?.pID ?? 1).ToString());
            command = command.Replace("<PC>", playerList.Length.ToString());
            command = command.Replace("<RI>", repeatIndex.ToString());
            // .Replace().Replace() ...
            if (!command.Contains('<') && !command.Contains('>'))
                return new [] { command };
            List<string> ResultCommandsList = new List<string>();
            if (command.Contains("<ALL>"))
                foreach (var pl in playerList)
                    ResultCommandsList.Add(command.Replace("<ALL>", pl.pID.ToString()));
            if (command.Contains("<AEM>"))  // All exclude me
                foreach (var pl in playerList)
                    if (pl.pID != (Me?.pID ?? 1))
                        ResultCommandsList.Add(command.Replace("<AEM>", pl.pID.ToString()));
            if (command.Contains("<T0>"))
                foreach (var pl in teams[0])
                    ResultCommandsList.Add(command.Replace("<T0>", pl.pID.ToString()));
            if (command.Contains("<T1>"))
                foreach (var pl in teams[1])
                    ResultCommandsList.Add(command.Replace("<T1>", pl.pID.ToString()));
            if (command.Contains("<T2>"))
                foreach (var pl in teams[2])
                    ResultCommandsList.Add(command.Replace("<T2>", pl.pID.ToString()));
            return ResultCommandsList.ToArray();
        }

        private void ExecuteScenarioAction(SARScenario.ScenarioAction sa, SARPlayer[] playerList, List<List<SARPlayer>> teams, int repeatIndex = 0)
        {   
            if (sa is SARScenario.ScenarioActionChatMessage)
            {
                var cmdText = ((SARScenario.ScenarioActionChatMessage)(sa)).text;
                var cmdsList = ParseScenarioTemplates(cmdText, playerList, teams, repeatIndex);
                foreach (var cmd in cmdsList)
                    ChatInput(cmd);
            }
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
            var playerList = GetPlayers();
            var _ = Me;
            List<List<SARPlayer>> Teams = new List<List<SARPlayer>>();
            if (scenario.NeedSelectTeams)
            {
                var frm = new FormTeamSelect(playerList);
                frm.ShowDialog();
                Teams.Add(frm.NoTeam);
                Teams.Add(frm.Team1);
                Teams.Add(frm.Team2);
            }
            new Task(() =>
            {
                ChatInput(new[] { "SARPMO: Start Scenario {\"}" + scenario.Title + "{\"}" });
                int count = 1;
                foreach (var sa in scenario.Queue)
                {
                    if (sa is SARScenario.ScenarioActionRepeat)
                    {
                        int.TryParse(ParseScenarioTemplates(((SARScenario.ScenarioActionRepeat)sa).param, playerList, Teams, 0)[0], out count);
                        continue;
                    }
                    for (int i = 0; i < count; i++)
                        ExecuteScenarioAction(sa, playerList, Teams, i);
                    do
                    {
                        Task.Delay(100).Wait();
                    }
                    while (ScenarioPause);
                    count = 1;
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
