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
        private static byte[] entropy;
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "KronoxBotBooker/files/file.");

        static SettingsManager()
        {
            entropy = new byte[5];
            entropy[0] = 222;
            entropy[1] = 2;
            entropy[2] = 33;
            entropy[3] = 112;
            entropy[4] = 001;
        }

        /// <summary>
        /// Reads all json entries from json file and returns them in a list.
        /// </summary>
        /// <returns></returns>
        public static List<JsonSettings> ReadSettings()
        {
            byte[] fileCipher = File.ReadAllBytes(path);
            byte[] plaintextDecypt = ProtectedData.Unprotect(fileCipher, entropy, DataProtectionScope.CurrentUser);
            var str = System.Text.Encoding.Default.GetString(plaintextDecypt);
            return JsonConvert.DeserializeObject<JsonSettings[]>(str).ToList<JsonSettings>();
        }

        /// <summary>
        /// Deletes the settings file.
        /// </summary>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        internal static void DeleteSettings()
        {
            File.Delete(path);
        }

        /// <summary>
        /// Adds a new settings entry to json file.
        /// </summary>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        public static bool WriteSettings(JsonSettings jsonSettings)
        {
            var settings = File.Exists(path) ? ReadSettings() : new List<JsonSettings>();

            if (settings.Contains(jsonSettings))
                return false; //Need to check this.

            settings.Add(jsonSettings);

            var json = JsonConvert.SerializeObject(settings);
            byte[] ciphertext = ProtectedData.Protect(Encoding.UTF8.GetBytes(json), entropy,
               DataProtectionScope.CurrentUser);

            File.WriteAllBytes(path, ciphertext);
            return true;
        }

        /// <summary>
        /// Updates the settings file with a new list of settings.
        /// </summary>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        public static bool UpdateSettings(List<JsonSettings> jsonSettings)
        {
            DeleteSettings();

            var json = JsonConvert.SerializeObject(jsonSettings);
            byte[] ciphertext = ProtectedData.Protect(Encoding.UTF8.GetBytes(json), entropy,
               DataProtectionScope.CurrentUser);

            File.WriteAllBytes(path, ciphertext);
            return true;
        }
    }
}
