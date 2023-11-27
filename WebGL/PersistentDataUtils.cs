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
        Debug.LogFormat("writing data to {0}", fileName);
        var contents = JsonUtility.ToJson(data, false);
        Debug.LogFormat("playerprefs set string {0}: {1}", fileName, contents);
        PlayerPrefs.SetString(fileName, contents);
        Debug.LogFormat("done");
        return Task.CompletedTask;
    }

    private static Task<T> LoadImpl<T>(string fileName)
    {
        Debug.LogFormat("reading data from {0}", fileName);
        var contents = PlayerPrefs.GetString(fileName);
        Debug.LogFormat("playerprefs get string {0}: {1}", fileName, contents);
        if (string.IsNullOrEmpty(contents))
        {
            Debug.LogFormat("none");
            return Task.FromResult<T>(default);
        }

        var data = JsonUtility.FromJson<T>(contents);
        Debug.LogFormat("done");
        return Task.FromResult<T>(data);
    }
}
