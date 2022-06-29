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
        public struct ScenarioActionRepeat : ScenarioAction { public string param; };

        public List<ScenarioAction> Queue { get; }
        public string Title { get; }
        public bool NeedSelectTeams { get; private set; } = false;

        public SARScenario(string title, List<ScenarioAction> queue, bool needSelectTeams)
        {
            Queue = queue;
            Title = title;
            NeedSelectTeams = needSelectTeams;
        }

        public new static SARScenario Parse(string str)
        {
            List<ScenarioAction> list = new List<ScenarioAction>();
            string? title = null;
            var NeedSelectTeams = false;
            var RepeatsCount = 1;
            var lines = str.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(line => !line.StartsWith("#")).Select(line => line.Split('\t'));
            foreach (var line in lines)
            {
                ScenarioAction? NextAction = null;

                if (line.First() == "R") // Repeats
                {
                    // if (int.TryParse(line[1], out RepeatsCount))
                    //     continue;
                    // else
                    NextAction = new ScenarioActionRepeat() { param = line[1] };
                }
                else if (line.First() == "D") // Delay
                    if (line[1] == "*") // Until key press
                        NextAction = new ScenarioActionPause();
                    else // Timer
                        NextAction = new ScenarioActionDelay() { seconds = float.Parse(line[1]) };
                else if (line.First() == "C") // Chat / Command
                {
                    if (!NeedSelectTeams && (line[1].Contains("<T1>") || line[1].Contains("<T2>") || line[1].Contains("<T0>") || line[1].Contains("<TA>")))
                        NeedSelectTeams = true;
                    NextAction = new ScenarioActionChatMessage() { text = line[1] };
                }
                else if (line.First() == "P") // Press key
                    NextAction = new ScenarioActionKeyboardInput() { keys = line[1] };
                else if (line.First() == "T") // Title
                    title = line[1];
                else if (line.First() == "S") // Start match
                    NextAction = new ScenarioActionStartMatch() { bots = line[1] == "+" };

                if (NextAction != null)
                    for (int i = 0; i < RepeatsCount; i++)
                        list.Add(NextAction);
                RepeatsCount = 1;

                // TODO: R - repeat (loop)
                // <PC> - players count

                // TODO: Q - select players: opens window to choose player
                // TODO: R - remove not selected players
                // TODO: W - select weapons
                // TODO: U - put selected weapons
                // TODO: replace <PX> to id, where X - selected player index
                // TODO: T - select teams: several players for several commands
                // TODO: replace <TX> with several commands: <P1> <P2> and etc for all players in current team
                // TODO: replace <ME> to yours ID
            }
            return new SARScenario(title ?? "Unnamed scenario", list, NeedSelectTeams);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
