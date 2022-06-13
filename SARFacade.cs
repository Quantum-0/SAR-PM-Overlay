using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAR_Overlay
{
    /*
    public class SARWeapon : SARParseble
    {
        public enum SARRarety : int
        {
            Common = 0,
            Uncommon = 1,
            Rare = 2,
            Epic = 3,
            Legendary = 4,
        }

        public SARRarety Rarety;
        private Image image = null;
        public readonly string Title;
        public readonly uint Index;
        private string imageFilename;

        public Image Image
        {
            get
            {
                if (image == null)
                    LoadImage();
                return image;
            }
        }

        public string Command
        {
            get
            {
                return $"/gun{Index} {(int)Rarety}";
            }
        }

        private void LoadImage()
        {
            try
            {
                image = Image.FromFile(imageFilename);
            }
            catch
            {

            }
        }
    }
    */

    public class SARLocation : SARParseble
    {
        public static readonly Size mapSize = new Size(4600, 4600);
        private static readonly Regex parser = new Regex(@"(\d{1,4}) (\d{1,4}) - (.*)");

        public Point Coords;
        public string Title;

        public string Square
        {
            get
            {
                return $"{(char)(65 + 8 * Coords.X / mapSize.Width)}{(8 - 8 * Coords.Y / mapSize.Height)}";
            }
        }

        public new static SARLocation Parse(string str)
        {
            var values = str.Split('\t').ToArray();
            Match match = parser.Match(str);
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            var title = match.Groups[3].Value;
            return new SARLocation() { Coords = new Point(x, y), Title = title };
        }

        public override string ToString()
        {
            return $"[{Square}] {Title} ({Coords.X}, {Coords.Y})";
        }
    }

    public class SARPlayer : SARParseble
    {
        public int pID;
        public string Name;
        public string PlayfabID;
        public bool isBot
        {
            get => String.IsNullOrWhiteSpace(PlayfabID);
        }

        public new static SARPlayer Parse(string str)
        {
            var values = str.Split('\t').ToArray();
            var pID = int.Parse(values[0]);
            return new SARPlayer() { pID = pID, Name = values[1], PlayfabID = values[2] };
        }

        public override string ToString()
        {
            return $"[{pID}] {Name}";
        }
    }

    public class SARScenario : SARParseble
    {
        public interface ScenarioAction { }
        public struct ScenarioActionChatMessage : ScenarioAction { public string text; }
        public struct ScenarioActionDelay : ScenarioAction { public float seconds; }
        public struct ScenarioActionKeyboardInput : ScenarioAction { public string keys; }
        public struct ScenarioActionStartMatch : ScenarioAction { public bool bots; }
        public struct ScenarioActionTitle : ScenarioAction { public string title; }

        public List<ScenarioAction> Queue;
        public string Title;

        public new static SARScenario Parse(string str)
        {
            List<ScenarioAction> list = new List<ScenarioAction>();
            string title = null;
            var lines = str.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(line => !line.StartsWith("#")).Select(line => line.Split('\t'));
            foreach (var line in lines)
            {
                if (line.First() == "D") // Delay
                    list.Add(new ScenarioActionDelay() { seconds = float.Parse(line[1]) });
                else if (line.First() == "C") // Chat / Command
                    list.Add(new ScenarioActionChatMessage() { text = line[1] });
                else if (line.First() == "P") // Press key
                    list.Add(new ScenarioActionKeyboardInput() { keys = line[1] });
                else if (line.First() == "T") // Press key
                    title = line[1];
                else if (line.First() == "S") // Start match
                    list.Add(new ScenarioActionStartMatch() { bots = line[1] == "+" });

                // TODO: Q - select players: opens window to choose player
                // TODO: R - remove not selected players
                // TODO: W - select weapons
                // TODO: U - put selected weapons
                // TODO: replace <PX> to id, where X - selected player index
                // TODO: T - select teams: several players for several commands
                // TODO: replace <TX> with several commands: <P1> <P2> and etc for all players in current team
                // TODO: replace <ME> to yours ID
            }
            return new SARScenario() { Queue = list, Title = title ?? "Unnamed scenario"};
        }
    }

    public class SARFacade
    {

        public const string WINDOW_NAME = "Super Animal Royale";
        IntPtr hWnd;
        const int delayAfterRefocusToSARWindow = 30;
        const int delayForChatOpening = 20;

        private SARFacade(IntPtr window_handle)
        {
            hWnd = window_handle;
        }

        public static SARFacade CreateFacade()
        {
            var h = NativeMethods.FindWindow(null, WINDOW_NAME);
            if (h.ToInt32() != 0)
                return new SARFacade(h);
            else
                return null;
        }

        public bool ChatInput(string[] commands)
        {
            return commands.Select(line => ChatInput(line)).All(a => a);
        }

        public bool ChatInput(string command)
        {
            if (NativeMethods.SetForegroundWindow(hWnd))
            {
                Task.Delay(delayAfterRefocusToSARWindow).Wait();
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                Task.Delay(delayForChatOpening).Wait();
                System.Windows.Forms.SendKeys.SendWait(command + "{ENTER}");
                return true;
            }
            return false;
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
                ChatInput(new[] { "SARPMO: Start Scenario \"" + scenario.Title + "\"" });
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

        public bool Teleport(Point location, int player_id = 1)
        {
            return ChatInput($"/tele {player_id} {location.X} {location.Y}");
        }

        public bool SwitchNight()
        {
            return ChatInput($"/night");
        }

        public bool Soccer()
        {
            return ChatInput($"/soccer");
        }

        private bool botsEnabled = false;
        public bool BotsEnabled
        {
            set
            {
                if (!started)
                    botsEnabled = value;
            }
            get => botsEnabled;
        }

        private bool started = false;

        public bool Started
        {
            get => started;
        }

        public bool Start(bool? withBots = null)
        {
            if (started)
                return false;

            if (withBots.HasValue)
                botsEnabled = withBots.Value;

            if (ChatInput(botsEnabled ? "/start" : "/startp"))
                started = true;

            ChatInput(new[] { "Welcome to Private Match", " controlled by SARPMO", " made by Quantum0 (aka Eat Me OwO)", "Good Luck and Have Fun! UwU" });
            return started;
        }

        private float gasSpeed = 1;
        public float GasSpeed
        {
            set
            {
                if (started)
                    return;
                value = (float)Math.Min(3.0, Math.Max(value, 0.4));
                if (ChatInput($"/gasspeed {value}"))
                    gasSpeed = value;
            }
            get => gasSpeed;
        }

        private bool gasOn = true;
        private bool gasStarted = false;
        public bool GasOn
        {
            set
            {
                if (gasStarted)
                    return;

                if (started)
                {
                    if (ChatInput(value ? "/gason 1" : "/gasoff"))
                    {
                        gasOn = value;
                        gasStarted = value;
                    }
                }
                else
                {
                    if (ChatInput(value ? "/gason" : "/gasoff"))
                        gasOn = value;
                }
            }
            get => gasOn;
        }

        private float gasDamage = 1;
        public float GasDamage
        {
            set
            {
                value = (float)Math.Min(10, Math.Max(value, 1));
                if (ChatInput($"/gasdmg  {value}"))
                    gasDamage = value;
            }
            get => gasDamage;
        }

        public SARPlayer[] GetPlayers()
        {
            if (!ChatInput("/getplayers"))
                return null;

            return Clipboard.GetText().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(line => SARPlayer.Parse(line)).ToArray();
        }

        public bool SetFocusOnGameWindows()
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }
    }
}
