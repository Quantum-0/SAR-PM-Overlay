using System;
using System.Windows;

namespace SAR_Overlay
{
    public partial class SARFacade
    {
        #region IN LOBBY

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

        /// <summary> Can be used to regenerate the Eagle flight path. Must run before the game timer has started. </summary>
        public bool Flight()
        {
            if (started)
                return false;
            return ChatInput("/flight");
        }

        // TODO
        /*
         * gasspeed
         * emus
         * utils
         * allitems
         * guns
         * throwables
         * armors
         * moles
         * hamballs
         */

        /// <summary> Spawns a Fox Ball (only 1 at a time). </summary>
        public bool Soccer()
        {
            if (started)
                return false;
            return ChatInput("/soccer");
        }

        /*
         * highping 
         * pets
         */

        #endregion

        #region BOTH LOBBY AND GAME

        public bool MatchID()
        {
            return ChatInput("/matchid");
        }

        /*
         * rainoff
         */

        /// <summary> Toggles the night mode. </summary>
        public bool Night()
        {
            return ChatInput("/night");
        }

        /*
         * getpid
         */

        /// <summary> Kicks player with specified in-game id #. Player cannot rejoin until next match. </summary>
        public bool Kick(int player_id)
        {
            return ChatInput($"/kick {player_id}");
        }

        /// <summary> Kicks player with specified in-game id #. Player cannot rejoin until next match. </summary>
        public bool Kick(SARPlayer player)
        {
            return ChatInput($"/kick {player.pID}");
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

        /// <summary> Teleports player with id # to X and Y world position. Maximum values are 4600, 4600. </summary>
        public bool Teleport(Point location, SARPlayer player)
        {
            return ChatInput($"/tele {player.pID} {location.X} {location.Y}");
        }

        /// <summary> Teleports player with id # to X and Y world position. Maximum values are 4600, 4600. </summary>
        public bool Teleport(SARLocation location, SARPlayer player)
        {
            return ChatInput($"/tele {player.pID} {location.Coords.X} {location.Coords.Y}");
        }

        /*
         * getpos 
         */

        /// <summary> Goes into spectate ghost mode. Can be run in lobby, or in-game after death only. </summary>
        public bool Ghost(SARPlayer player)
        {
            return ChatInput($"/ghost {player.pID}");
        }

        /*
         * gasoff
         * gason
         * gasstart
         * gasdmg 
         * bulletspeed
         * onehits
         */

        /// <summary> Set a player to god-mode with ID #. Applies only to player damage. </summary>
        public bool God(SARPlayer player)
        {
            return ChatInput($"/god {player.pID}");
        }

        #endregion

        #region IN GAME

        /*
         * rain
         * rainoff
         * 
         */

        /// <summary> Kills player (or bot) with specified in-game id  </summary>
        public bool Kill(int player_id)
        {
            if (!started)
                return false;
            return ChatInput($"/kill {player_id}");
        }

        /// <summary> Kills player (or bot) with specified in-game id  </summary>
        public bool Kill(SARPlayer player)
        {
            if (!started)
                return false;
            return ChatInput($"/kill {player.pID}");
        }

        /// <summary> Makes an amount of BananaUI.png Banana pickups spawn near you (max 10) </summary>
        public bool Banana(int amount = 1)
        {
            if (!started)
                return false;
            return ChatInput($"/banana {amount}");
        }

        /*
         * gun
         * util
         */

        /// <summary> Makes given player infected. </summary>
        public bool Infect(SARPlayer player)
        {
            return ChatInput($"/infect {player.pID}");
        }

        /// <summary> Spawns ammo type with a specified value. </summary>
        public bool Ammo(Enums.SARAmmo type, int? amount)
        {
            if (!started)
                return false;
            return ChatInput(amount.HasValue ? $"/ammo{(int)type} {amount}" : $"/ammo{(int)type}");
        }

        /// <summary> Spawns Armor of selected level </summary>
        public bool Armor(int level = 3)
        {
            if (level > 3 || level < 1)
                throw new ArgumentException();
            if (!started)
                return false;
            return ChatInput($"/armor{level}");
        }

        /// <summary> Makes a Hamster Ball spawn near you. </summary>
        public bool Hamball()
        {
            if (!started)
                return false;
            return ChatInput("/hamball");
        }

        /// <summary> Spawns Health Juice </summary>
        public bool Juice(int amount = 40)
        {
            if (!started)
                return false;
            return ChatInput($"/juice {amount}");
        }

        /// <summary> Spawns Super Tape </summary>
        public bool Tape(int amount = 3)
        {
            if (!started)
                return false;
            return ChatInput($"/tape {amount}");
        }
    }
}
