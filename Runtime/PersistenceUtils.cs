//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lumpn.Persistence
{
    public static class PersistenceUtils
    {
        public static Task SaveAsync<T>(string fileName, T data)
        {
            var filePath = GetFilePath(fileName);
            var contents = JsonUtility.ToJson(data, true);
            return File.WriteAllTextAsync(filePath, contents, Encoding.UTF8);
        }

        public static async Task<T> LoadAsync<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);
            if (File.Exists(filePath))
            {
                var contents = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
                return JsonUtility.FromJson<T>(contents);
            }
            return default;
        }

        private static string GetFilePath(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}
