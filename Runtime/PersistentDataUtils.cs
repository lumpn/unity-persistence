using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PersistentDataUtils
{
    public static Task Save<T>(string fileName, T data)
    {
        try
        {
            return SaveImpl<T>(fileName, data);
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }
        return Task.CompletedTask;
    }

    public static Task<T> Load<T>(string fileName)
    {
        try
        {
            return LoadImpl<T>(fileName);
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }
        return Task.FromResult<T>(default);
    }

    private static Task SaveImpl<T>(string fileName, T data)
    {
        var filePath = GetFilePath(fileName);
        var contents = JsonUtility.ToJson(data, true);
        return File.WriteAllTextAsync(filePath, contents, Encoding.UTF8);
    }

    private static async Task<T> LoadImpl<T>(string fileName)
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
