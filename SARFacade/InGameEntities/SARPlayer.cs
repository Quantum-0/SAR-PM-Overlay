using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SAR_Overlay
{
    /// <summary> Represents player received from /getplayers </summary>
    public class SARPlayer : SARParseble
    {
        private new static readonly Regex Parser = new Regex(@"(\d{1,2})\t(.*)\t(.*)");

        /// <summary> In-match player's ID </summary>
        public int pID { get; }
        /// <summary> Player's name </summary>
        public string Name { get; }
        /// <summary> Player's Steam ID. Empty for bots </summary>
        public string? PlayfabID { get; }
        /// <summary> "Player is bot" property </summary>
        public bool IsBot { get => String.IsNullOrWhiteSpace(PlayfabID); }

        protected SARPlayer(int pID, string name, string? playfabID)
        {
            this.pID = pID;
            Name = name;
            PlayfabID = playfabID;
        }

        /// <summary> Parses string "ID\tName\tPrefabID" into player object </summary>
        public new static SARPlayer Parse(string str)
        {
            Match match = Parser.Match(str);
            var pID = int.Parse(match.Groups[1].Value);
            return new SARPlayer(pID, match.Groups[2].Value, match.Groups[3].Value);
        }

        public static implicit operator int(SARPlayer p) => p.pID;

        public override string ToString() => $"[{pID}] {Name}";

        public SARPlayerWithTeam WithTeam(int team)
        {
            var plwt = new SARPlayerWithTeam(this);
            plwt.Team1 = team == 1;
            plwt.Team2 = team == 2;
            plwt.NoTeam = team == 0;
            return plwt;
        }
    }

    public class SARPlayerWithTeam : SARPlayer
    {
        public bool Team1 { set; get; } = false;
        public bool Team2 { set; get; } = false;
        public bool NoTeam { set; get; } = true;

        private SARPlayerWithTeam(int pID, string name, string playfabID) : base(pID, name, playfabID)
        {

        }

        public SARPlayerWithTeam(SARPlayer player) : base(player.pID, player.Name, player.PlayfabID)
        {

        }
    }
}
