using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAR_Overlay
{
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

        public bool SetFocusOnGameWindows()
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }
    }
}
