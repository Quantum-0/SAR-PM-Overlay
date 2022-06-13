using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAR_Overlay
{
    // TODO:
    // Scenarious - config/scenarious/*.sarpms (SAR Private Match Scenario
    // Locations - config/locations.txt
    // What else?
    
    public static class Config
    {
        private const string locationsFileName = "Locations.txt";
        private const string scenariousFolder = "Scenarios";
        private static string configDir = "Config/";

        private static SARLocation[] teleportLocations = null;
        public static SARLocation[] TeleportLocations
        {
            get
            {
                if (teleportLocations == null)
                    ReloadTeleportLocations();
                return teleportLocations;
            }
        }

        private static SARScenario[] scenariosList = null;
        public static SARScenario[] ScenariosList
        {
            get
            {
                if (scenariosList == null)
                    ReloadScenariosList();
                return scenariosList;
            }
        }

        private static T[] LoadFromFile<T>(string filename) where T : SARParseble
        {
            if (!File.Exists(filename))
                return new T[] { };

            try
            {
                return File.ReadAllLines(filename).Select(line => (T)typeof(T).GetMethod("Parse").Invoke(null, new object[] { line })).ToArray();
            }
            catch
            {
                MessageBox.Show("Error loading from file " + filename, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new T[] { };
            }
        }

        private static T[] LoadFromFolder<T>(string path, string fileMask) where T : SARParseble
        {
            if (!Directory.Exists(path))
                return new T[] { };

            try
            {
                var filenames = Directory.EnumerateFiles(path, fileMask, SearchOption.TopDirectoryOnly);
                var list = new List<T>();
                foreach (var fname in filenames)
                {
                    var scenarioText = File.ReadAllText(fname);
                    list.Add((T)typeof(T).GetMethod("Parse").Invoke(null, new object[] { scenarioText }));
                }
                return list.ToArray();
            }
            catch
            {
                return new T[] { };
            }
        }

        public static void ReloadScenariosList() => scenariosList = LoadFromFolder<SARScenario>(configDir + scenariousFolder, "*.sarpms");

        public static void ReloadTeleportLocations() => teleportLocations = LoadFromFile<SARLocation>(configDir + locationsFileName);
    }
}
