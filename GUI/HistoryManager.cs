using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KronoxScraperBotGUI
{
    /// <summary>
    /// Manages saving and reading stored username and password.
    /// </summary>
    internal class HistoryManager
    {
        private static string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Kronox Bot/files/up.");

        private static readonly byte[] _entropy = new byte[]
        {
            1,9,255,112,001,12,152,4,8,32
        };

        /// <summary>
        /// Reads User from file.
        /// </summary>
        /// <returns></returns>
        public static User ReadUser()
        {
            var fileEncrypted = File.ReadAllBytes(_path);
            var fileDecrypted = ProtectedData.Unprotect(fileEncrypted, _entropy, DataProtectionScope.CurrentUser);
            var fileAsString = Encoding.Default.GetString(fileDecrypted);
            return JsonConvert.DeserializeObject<User>(fileAsString);
        }

        /// <summary>
        /// Adds a new user to file.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static void WriteUser(User user)
        {
            DeleteUser();

            var json = JsonConvert.SerializeObject(user);
            byte[] fileEncrypted = ProtectedData.Protect(Encoding.UTF8.GetBytes(json), _entropy,
               DataProtectionScope.CurrentUser);

            File.WriteAllBytes(_path, fileEncrypted);
        }

        /// <summary>
        /// Deletes the user file.
        /// </summary>
        /// <param name="jsonSettings"></param>
        /// <returns></returns>
        public static void DeleteUser()
        {
            File.Delete(_path);
        }
    }
}