using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SimpleSerializer
{
    #region paths
    public static string ROOT = Application.dataPath + "/../";
    public static string PERSISTENT = Application.streamingAssetsPath;
    public static string SAVE = ROOT + "/Save";

    public static Environment Env = new Environment("game.config");
    #endregion

    const string FILE_EXTENSION = ".sav";

    public static void SaveInt(string key, int value)
    {
        string PATH = Environment.GetPath("save") + "/" + key + "INT" + FILE_EXTENSION;
        File.WriteAllText(PATH, value.ToString());
    }

    public static int LoadInt(string key)
    {
        string PATH = Environment.GetPath("save") + "/" + key + "INT" + FILE_EXTENSION;
        if (File.Exists(PATH))
        {
            string result = File.ReadAllText(PATH);
            return int.Parse(result);
        }
        else return 0;

    }

    public static void SaveFloat(string key, float value)
    {
        string PATH = Environment.GetPath("save") + "/" + key + "FLT" + FILE_EXTENSION;
        File.WriteAllText(PATH, value.ToString());
    }

    public static float LoadFloat(string key)
    {
        string PATH = Environment.GetPath("save") + "/" + key + "FLT" + FILE_EXTENSION;
        if (File.Exists(PATH)) {
            string result = File.ReadAllText(PATH);
            return float.Parse(result);
        }
        else return 0f;

    }

    public static void SaveString(string key, string value)
    {
        string PATH = Environment.GetPath("save") + "/" + key + "STR" + FILE_EXTENSION;
        File.WriteAllText(PATH, value);
    }

    public static string LoadString(string key)
    {
        string PATH = Environment.GetPath("save") + "/" + key + "STR" + FILE_EXTENSION;
        if (File.Exists(PATH))
        {
            return File.ReadAllText(PATH);
        }
        else return "";
        
    }

    public static void SaveVector(string key, Vector2 value) {

        string PATH = Environment.GetPath("save") + "/" + key + "VEC" + FILE_EXTENSION;
        string[] data = new string[2];
        data[0] = value.x.ToString();
        data[1] = value.y.ToString();
        File.WriteAllLines(PATH, data);
    }

    public static Vector2 LoadVector(string key)
    {
        string PATH = Environment.GetPath("save") + "/" + key + "VEC" + FILE_EXTENSION;
        if (File.Exists(PATH))
        {
            string[] data = File.ReadAllLines(PATH);
            float x = float.Parse(data[0]);
            float y = float.Parse(data[1]);

            return new Vector2(x, y);
        }
        else return Vector2.zero;

    }

    public static void ClearKey(string Key)
    {
        string[] extens = new string[] { "VEC", "INT", "FLT", "STR" };

        for (int i = 0; i < extens.Length; i++)
        {
            string PATH = Environment.GetPath("save") + "/" + Key + extens[i] + FILE_EXTENSION;
            if(File.Exists(PATH))
            {
                File.Delete(PATH);
            }
        }
    }

    public static bool IsFirstLoad()
    {
        if (File.Exists(Environment.GetPath("bin") + "/loc.q")) {
            return false;
        }
        else {
            File.Create(Environment.GetPath("bin") + "/loc.q");
            return true;
        }
    }
}
 
public static class util {
    public static Vector2 Minus(this Vector2 vector, Vector2 other)
    {
        return new Vector2(vector.x - other.x, vector.y - other.y);
    }
}
