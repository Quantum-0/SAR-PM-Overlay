using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR_Overlay
{
    public class SARScenario : SARParseble
    {
        public interface ScenarioAction { }
        public struct ScenarioActionChatMessage : ScenarioAction { public string text; }
        public struct ScenarioActionDelay : ScenarioAction { public float seconds; }
        public struct ScenarioActionKeyboardInput : ScenarioAction { public string keys; }
        public struct ScenarioActionStartMatch : ScenarioAction { public bool bots; }
        public struct ScenarioActionTitle : ScenarioAction { public string title; }
        public struct ScenarioActionPause : ScenarioAction { };

        public List<ScenarioAction> Queue { get; }
        public string Title { get; }

        public SARScenario(string title, List<ScenarioAction> queue)
        {
            Queue = queue;
            Title = title;
        }

        public new static SARScenario Parse(string str)
        {
            List<ScenarioAction> list = new List<ScenarioAction>();
            string? title = null;
            var lines = str.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(line => !line.StartsWith("#")).Select(line => line.Split('\t'));
            foreach (var line in lines)
            {
                if (line.First() == "D") // Delay
                    if (line[1] == "*") // Until key press
                        list.Add(new ScenarioActionPause());
                    else // Timer
                        list.Add(new ScenarioActionDelay() { seconds = float.Parse(line[1]) });
                else if (line.First() == "C") // Chat / Command
                    list.Add(new ScenarioActionChatMessage() { text = line[1] });
                else if (line.First() == "P") // Press key
                    list.Add(new ScenarioActionKeyboardInput() { keys = line[1] });
                else if (line.First() == "T") // Title
                    title = line[1];
                else if (line.First() == "S") // Start match
                    list.Add(new ScenarioActionStartMatch() { bots = line[1] == "+" });

                // TODO: Q - select players: opens window to choose player
                // TODO: R - remove not selected players
                // TODO: W - select weapons
                // TODO: U - put selected weapons
                // TODO: replace <PX> to id, where X - selected player index
                // TODO: T - select teams: several players for several commands
                // TODO: replace <TX> with several commands: <P1> <P2> and etc for all players in current team
                // TODO: replace <ME> to yours ID
            }
            return new SARScenario(title ?? "Unnamed scenario", list);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
