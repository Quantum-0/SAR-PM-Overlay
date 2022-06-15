using System.Windows;

namespace SAR_Overlay
{
    public partial class SARFacade
    {
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
            if (!started)
                return false;
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
            if (!started)
                return false;
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
    }
}
