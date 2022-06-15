﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SAR_Overlay
{
    public partial class SARFacade
    {
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

        private SARPlayer me;
        public SARPlayer Me
        {
            get
            {
                if (me == null)
                {
                    var players = GetPlayers();
                    if (players != null)
                        me = players.First(); // TODO: Won't work if player is not first, so won't works for moders
                }
                return me;
            }
        }

        public SARPlayer[]? GetPlayers()
        {
            if (!ChatInput("/getplayers"))
                return null;

            return Clipboard.GetText().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(line => SARPlayer.Parse(line)).ToArray();
        }
    }
}