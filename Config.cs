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
    abstract class SARParseble
    {
        public static SARParseble Parse(string str) => throw new NotImplementedException();
    }

    public static class Config
    {
        private const string locationsFileName = "Locations.txt";
        private const string scenariousFolder = "Scenarious";
        private static string configDir = "Config/";

        /*
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        static Config()
        {
            if (Path.GetDirectoryName(Application.ExecutablePath).EndsWith(@"bin\Debug") && !Directory.Exists(configDir) && Directory.Exists(Path.Combine("../..", configDir)))
            {
                Directory.CreateDirectory(configDir);
                CopyFilesRecursively(Path.Combine("../..", configDir), configDir);
            }
        }
        */

        private static SARLocation[] teleportLocations = null;
        public static SARLocation[] TeleportLocations
        {
            get
            {
                if (teleportLocations == null)
                    ReloadAll();
                return teleportLocations;
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

        /*
        private static T[] LoadFromFolder<T>(string path) where T : SARParseble
        {
            if (!Directory.Exists(path))
                return new T[] { };

            try
            {
                Directory.E
            }
        }

    */
        public static void ReloadAll()
        {
            teleportLocations = new SARLocation[0];

            if (Directory.Exists(configDir))
                if (File.Exists(configDir + locationsFileName))
                {
                    var locations = File.ReadAllText(configDir + locationsFileName);
                    teleportLocations = locations.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(loc => SARLocation.Parse(loc)).ToArray();
                }
            if (Path.GetDirectoryName(Application.ExecutablePath).EndsWith(@"bin\Debug"))
            {
                configDir = "../../" + configDir;
                if (Directory.Exists(configDir))
                    if (File.Exists(configDir + locationsFileName))
                    {
                        var locations = File.ReadAllText(configDir + locationsFileName);
                        teleportLocations = locations.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(loc => SARLocation.Parse(loc)).ToArray();
                    }
            }
        }
    }
}
