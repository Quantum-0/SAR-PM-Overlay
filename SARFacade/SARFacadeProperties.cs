using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR_Overlay
{
    public partial class SARFacade
    {
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

        private bool oneHits = false;
        /// <summary> Toggles one-hit kill mode. Doesn't apply to grenades or gas. </summary>
        public bool OneHits 
        {
            get => oneHits;
            set
            {
                if (oneHits != value)
                    if (ChatInput("/onehits"))
                        oneHits = value;
            }
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
                if (ChatInput($"/gasdmg {value.ToString().Replace(',', '.')}"))
                    gasDamage = value;
            }
            get => gasDamage;
        }
    }
}
