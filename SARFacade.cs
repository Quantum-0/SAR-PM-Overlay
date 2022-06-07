using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAR_Overlay
{
    public struct SARPlayer
    {
        int pID;
        string Name;
        string PlayfabID;
        bool isBot
        {
            get => String.IsNullOrWhiteSpace(PlayfabID);
        }

        public static SARPlayer Parse(string str)
        {
            var values = str.Split('\t').ToArray();
            var pID = int.Parse(values[0]);
            return new SARPlayer() { pID = pID, Name = values[1], PlayfabID = values[2] };
        }
    }

    public class SARFacade
    {

        public const string WINDOW_NAME = "Super Animal Royale";
        IntPtr hWnd;

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

        public bool ChatInput(string command)
        {
            if (NativeMethods.SetForegroundWindow(hWnd))
            {
                Task.Delay(50).Wait();
                SendKeys.SendWait("{ENTER}" + command + "{ENTER}");
                return true;
            }
            return false;
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
