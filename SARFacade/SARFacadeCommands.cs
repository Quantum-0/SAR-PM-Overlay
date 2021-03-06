using System;
using System.Windows;

// Commands' descriptions was copied from https://animalroyale.fandom.com/wiki/Guides/Private_Matches
// And distributed under CC BY-NC-SA 3.0 license

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

            var glhf = "Good Luck and Have Fun! UwU";
            if (rnd.Next(15) == 0)
                glhf = glhf.Replace("Luck", "Lick");
            if (rnd.Next(10) == 0)
                glhf = glhf.Replace("Fun", "Fur");
            if (rnd.Next(25) == 0)
                glhf = glhf.Replace("Fun", "Sun");
            if (rnd.Next(7) == 0)
                glhf = glhf.Replace("UwU", "OwO");
            if (rnd.Next(6) == 0)
                glhf = glhf.Replace("UwU", "=w=");
            if (rnd.Next(3) == 0)
                glhf = glhf.Replace("UwU", "<3");

            ChatInput(new[] { "Welcome to Private Match, controlled by SAR-PMO, made by Quantum0", glhf }, true);
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

        /// <summary> Shows the code that players can enter to join. </summary>
        public bool MatchID() => ChatInput("/matchid");

        /*
         * rainoff
         */

        /// <summary> Toggles the night mode. </summary>
        public bool Night() => ChatInput("/night");

        /// <summary> Shows your in-game player id #. </summary>
        public bool GetPID() => ChatInput("/getpid");

        /// <summary> Kicks player with specified in-game id #. Player cannot rejoin until next match. </summary>
        public bool Kick(int player_id) => ChatInput($"/kick {player_id}");

        /// <summary> Kicks player with specified in-game id #. Player cannot rejoin until next match. </summary>
        public bool Kick(SARPlayer player) =>  ChatInput($"/kick {player.pID}");

        /// <summary> Teleports player with id # to X and Y world position. Maximum values are 4600, 4600. </summary>
        public bool Teleport(Point location, int? player_id = null) => ChatInput($"/tele {player_id ?? Me.pID} {location.X} {location.Y}");

        /// <summary> Teleports player with id # to X and Y world position. Maximum values are 4600, 4600. </summary>
        public bool Teleport(SARLocation location, SARPlayer player) => ChatInput($"/tele {player.pID} {location.Coords.X} {location.Coords.Y}");

        /// <summary> Tells you world position of a player with given number </summary>
        public bool GetPos(SARPlayer? player = null) => started &&
            ChatInput(player != null ? $"/getpos {player.pID}" : $"/getpos");

        /// <summary> Goes into spectate ghost mode. Can be run in lobby, or in-game after death only. </summary>
        public bool Ghost(SARPlayer player) => ChatInput($"/ghost {player.pID}");

        /*
         * gasoff
         * gason
         * gasstart
         * gasdmg 
         * bulletspeed
         * onehits
         */

        /// <summary> Set a player to god-mode with ID #. Applies only to player damage. </summary>
        public bool God(SARPlayer player) => ChatInput($"/god {player.pID}");

        #endregion

        #region IN GAME

        /*
         * rain
         * rainoff
         * 
         */

        /// <summary> Kills player (or bot) with specified in-game id  </summary>
        public bool Kill(int player_id) => started && ChatInput($"/kill {player_id}");

        /// <summary> Kills player (or bot) with specified in-game id  </summary>
        public bool Kill(SARPlayer player) => started && ChatInput($"/kill {player.pID}");

        /// <summary> Makes an amount of BananaUI.png Banana pickups spawn near you (max 10) </summary>
        public bool Banana(int amount = 1) => started && ChatInput($"/banana {amount}");

        /// <summary> Makes a gun spawn near you, with specified gun_id. gun_id value must be from 0 to 17. </summary>
        public bool Gun(Enums.SARWeapon type, Enums.SARRarety? rarety = null) => started &&
            ChatInput(rarety != null ? $"/gun{(int)type} {(int)rarety}" : $"/gun{(int)type}");

        /// <summary> Makes a Super Powerup item spawn near you with specified #. Valid # is from 0 to 6 currently. </summary>
        public bool Util(Enums.SARUtil type) => started && ChatInput($"/util{(int)type}");

        /// <summary> Makes given player infected. </summary>
        public bool Infect(SARPlayer player) => started && ChatInput($"/infect {player.pID}");

        /// <summary> Spawns ammo type with a specified value. </summary>
        public bool Ammo(Enums.SARAmmo type, int? amount) => started &&
            ChatInput(amount.HasValue ? $"/ammo{(int)type} {amount}" : $"/ammo{(int)type}");

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
        public bool Hamball() => started && ChatInput("/hamball");

        /// <summary> Spawns Health Juice </summary>
        public bool Juice(int amount = 40) => started && ChatInput($"/juice {amount}");

        /// <summary> Spawns Super Tape </summary>
        public bool Tape(int amount = 3) => started && ChatInput($"/tape {amount}");

        #endregion
    }
}
