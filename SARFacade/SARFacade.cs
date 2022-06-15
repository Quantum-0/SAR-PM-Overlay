using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SAR_Overlay
{
    public class SARFacade
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

        public bool ChatInput(string[] commands, bool dontCheckLength = false)
        {
            return commands.Select(line => ChatInput(line, dontCheckLength)).All(a => a);
        }

        public bool ChatInput(string command, bool dontCheckLength = false)
        {
            if (!dontCheckLength && command.Length > maxChatMessageLength)
                throw new ArgumentException();
            Console.WriteLine($"Command: {command}");
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

        /// <summary> Teleports player with id # to X and Y world position. Maximum values are 4600, 4600. </summary>
        public bool Teleport(Point location, int player_id = 1)
        {
            return ChatInput($"/tele {player_id} {location.X} {location.Y}");
        }

        /// <summary> Teleports player with id # to X and Y world position. Maximum values are 4600, 4600. </summary>
        public bool Teleport(SARLocation location, int player_id = 1)
        {
            return ChatInput($"/tele {player_id} {location.Coords.X} {location.Coords.Y}");
        }

        /// <summary> Kills player (or bot) with specified in-game id  </summary>
        public bool Kill(int player_id)
        {
            return ChatInput($"/kill {player_id}");
        }

        /// <summary> Kicks player with specified in-game id #. Player cannot rejoin until next match. </summary>
        public bool Kick(int player_id)
        {
            return ChatInput($"/kick {player_id}");
        }

        /// <summary> Makes an amount of BananaUI.png Banana pickups spawn near you (max 10) </summary>
        public bool Banana(int amount = 1)
        {
            return ChatInput($"/banana {amount}");
        }

        /// <summary> Toggles the night mode. </summary>
        public bool SwitchNight()
        {
            return ChatInput("/night");
        }

        /// <summary> Spawns a Fox Ball (only 1 at a time). </summary>
        public bool Soccer()
        {
            if (started)
                return false;
            return ChatInput("/soccer");
        }

        /// <summary> Can be used to regenerate the Eagle flight path. Must run before the game timer has started. </summary>
        public bool Flight()
        {
            if (started)
                return false;
            return ChatInput("/flight");
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

        /// <summary> Starts lobby countdown timer (~18s countdown). </summary>
        /// <param name="withBots">Bots are added to fill player slots.</param>
        public bool Start(bool? withBots = null)
        {
            if (started)
                return false;

            if (withBots.HasValue)
                botsEnabled = withBots.Value;

            if (ChatInput(botsEnabled ? "/start" : "/startp"))
                started = true;

            ChatInput(new[] { "Welcome to Private Match, controlled by SAR-PMO, made by Eat Me OwO", " {(}aka Quantum0{)}                        Good Luck and Have Fun! UwU" }, true);
            return started;
        }

        private float gasSpeed = 1;
        /// <summary> Speed multiplier of Skunk Gas. Value can be from 0.4 to 3.0 </summary>
        public float GasSpeed
        {
            set
            {
                if (started)
                    return;
                value = (float)Math.Round(Math.Min(3.0, Math.Max(value, 0.4)), 1);
                if (ChatInput($"/gasspeed {value.ToString().Replace(',', '.')}"))
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
                value = (float)Math.Round(Math.Min(10, Math.Max(value, 1)), 1);
                if (ChatInput($"/gasdmg {value.ToString().Replace(',','.')}"))
                    gasDamage = value;
            }
            get => gasDamage;
        }

        public SARPlayer[]? GetPlayers()
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
