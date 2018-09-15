using System.IO;
using System.Xml.Serialization;

namespace LearnPoems.Settings
{
    public class Settings
    {
        private static XmlSerializer serialiser;

        static Settings()
        {
            serialiser = new XmlSerializer(typeof(Settings));
        }

        public string PoemFolder { get; set; }

        public string SystemFolder { get; set; }

        /// <summary> Save these settings</summary>
        /// <param name="settingsFilePath"></param>
        public void Save(string settingsFilePath)
        {
            SaveSettings(this, settingsFilePath);
        }

        #region Static methods

        /// <summary> Get the Settings file. Create a blank one if not found</summary>
        /// <param name="settingsPath"></param>
        /// <returns></returns>
        public static Settings GetSettings(string settingsPath)
        {
            Settings settings = new Settings();
            if (!File.Exists(settingsPath))
            {
                SaveSettings(settings, settingsPath);
            }
            else
            {
                using (TextReader reader = new StreamReader(settingsPath))
                {
                    settings = (Settings)serialiser.Deserialize(reader);
                }
            }
            return settings;
        }

        /// <summary> Save the Settings to a file </summary>
        /// <param name="settings"></param>
        /// <param name="settingsFilePath"></param>
        public static void SaveSettings(Settings settings, string settingsFilePath)
        {
            // Create writer that will overwrite or create (rather than append to existing file)
            using (TextWriter writer = new StreamWriter(settingsFilePath, false))
            {
                serialiser.Serialize(writer, settings);
            }
        }

        #endregion Static methods

    }
}
