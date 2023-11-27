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

            Debug.LogFormat("writing data to {0}", filePath);
            var contents = JsonUtility.ToJson(data, false);
            Debug.LogFormat("playerprefs set string {0}: {1}", filePath, contents);
            PlayerPrefs.SetString(filePath, contents);
            Debug.LogFormat("done");
            return Task.CompletedTask;
        }

        public static Task<T> LoadAsync<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);

            Debug.LogFormat("reading data from {0}", filePath);
            var contents = PlayerPrefs.GetString(filePath);
            Debug.LogFormat("playerprefs get string {0}: {1}", filePath, contents);
            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogFormat("none");
                return Task.FromResult<T>(default);
            }

            var data = JsonUtility.FromJson<T>(contents);
            Debug.LogFormat("done");
            return Task.FromResult<T>(data);
        }

        private static string GetFilePath(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}
