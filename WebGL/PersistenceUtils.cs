//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Lumpn.Persistence
{
    public static class PersistenceUtils
    {
        public static Task SaveAsync<T>(string fileName, T data)
        {
            var filePath = GetFilePath(fileName);
            var contents = JsonUtility.ToJson(data, false);
            PlayerPrefs.SetString(filePath, contents);
            return Task.CompletedTask;
        }

        public static Task<T> LoadAsync<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);
            var contents = PlayerPrefs.GetString(filePath);
            if (string.IsNullOrEmpty(contents))
            {
                return Task.FromResult<T>(default);
            }

            var data = JsonUtility.FromJson<T>(contents);
            return Task.FromResult<T>(data);
        }

        private static string GetFilePath(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}
