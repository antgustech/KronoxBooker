using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Class that reads settings from an encrypted file, builds settings objects and returns them as a list.
    /// </summary>
    public static class SettingsManager
    {
        private static string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Kronox Bot/files/file.");
        private static readonly byte[] _entropy = new byte[]
        {
            222,2,33,112,001
        };

        /// <summary>
        /// Reads all json entries from json file and returns them in a list.
        /// </summary>
        /// <returns></returns>
        public static List<JsonSettings> ReadSettings()
        {
            var fileEncrypted = File.ReadAllBytes(_path);
            var fileDecrypted = ProtectedData.Unprotect(fileEncrypted, _entropy, DataProtectionScope.CurrentUser);
            var fileAsString = Encoding.Default.GetString(fileDecrypted);
            return JsonConvert.DeserializeObject<JsonSettings[]>(fileAsString).ToList<JsonSettings>();
        }

        /// <summary>
        /// Adds a new settings entry to json file.
        /// </summary>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        public static bool WriteSettings(JsonSettings jsonSettings)
        {
           var settings = File.Exists(_path) ? ReadSettings() : new List<JsonSettings>();
           var exists =  settings.Where(s => s.TimeInterval == jsonSettings.TimeInterval && s.BuildingDesignation == jsonSettings.BuildingDesignation && s.Username == jsonSettings.Username);

            if (exists.Count() > 0)
                return false;

            settings.Add(jsonSettings);
            UpdateSettings(settings);
            return true;
        }

        /// <summary>
        /// Updates the settings file with a new list of settings.
        /// </summary>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        public static void UpdateSettings(List<JsonSettings> jsonSettings)
        {
            DeleteSettings();

            var json = JsonConvert.SerializeObject(jsonSettings);
            byte[] fileEncrypted = ProtectedData.Protect(Encoding.UTF8.GetBytes(json), _entropy,
               DataProtectionScope.CurrentUser);

            File.WriteAllBytes(_path, fileEncrypted);
        }

        /// <summary>
        /// Deletes the settings file.
        /// </summary>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        public static void DeleteSettings()
        {
            File.Delete(_path);
        }
    }
}
