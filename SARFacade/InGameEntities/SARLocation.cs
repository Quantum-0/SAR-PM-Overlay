using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace SAR_Overlay
{
    /// <summary> Represents location on the Map in SAR </summary>
    public class SARLocation : SARParseble
    {
        public static readonly Size mapSize = new Size(4600, 4600);
        private new static readonly Regex Parser = new Regex(@"(\d{1,4}) (\d{1,4}) - (.*)");

        /// <summary> Coordinates on the map </summary>
        public Point Coords { get; }
        /// <summary> Title of the location </summary>
        public string Title { get; }
        /// <summary> Square code (from A1 to H8) for point on the map </summary>
        public string Square { get => $"{(char)(65 + 8 * Coords.X / mapSize.Width)}{(8 - 8 * Coords.Y / mapSize.Height)}"; }

        private SARLocation(string title, Point coords)
        {
            Title = title;
            Coords = coords;
        }

        /// <summary> Parses string "X Y - Title" into location object </summary>
        public new static SARLocation Parse(string str)
        {
            Match match = Parser.Match(str);
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            var title = match.Groups[3].Value;
            return new SARLocation(title, new Point(x, y));
        }

        public override string ToString() => $"[{Square}] {Title} ({Coords.X}, {Coords.Y})";
    }
}
