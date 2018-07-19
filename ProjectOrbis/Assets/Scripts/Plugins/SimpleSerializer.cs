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

    public static void SaveInt(string key, int value)
    {
        string PATH = Environment.GetPath("save") + "/" + key + ".txt";
        File.WriteAllText(PATH, value.ToString());
    }

    public static int LoadInt(string key)
    {
        string PATH = Environment.GetPath("save") + "/" + key + ".txt";
        if (File.Exists(PATH))
        {
            string result = File.ReadAllText(PATH);
            return int.Parse(result);
        }
        else return 0;

    }

    public static void SaveString(string key, string value)
    {
        string PATH = Environment.GetPath("save") + "/" + key + ".txt";
        File.WriteAllText(PATH, value);
    }

    public static string LoadString(string key)
    {
        string PATH = Environment.GetPath("save") + "/" + key + ".txt";
        if (File.Exists(PATH))
        {
            return File.ReadAllText(PATH);
        }
        else return "";
        
    }

    public static void SaveVector(string key, Vector2 value) {

        string PATH = Environment.GetPath("save") + "/" + key + ".txt";
        string[] data = new string[2];
        data[0] = value.x.ToString();
        data[1] = value.y.ToString();
        File.WriteAllLines(PATH, data);
    }

    public static Vector2 LoadVector(string key)
    {
        string PATH = Environment.GetPath("save") + "/" + key + ".txt";
        if (File.Exists(PATH))
        {
            string[] data = File.ReadAllLines(PATH);
            float x = float.Parse(data[0]);
            float y = float.Parse(data[1]);

            return new Vector2(x, y);
        }
        else return Vector2.zero;

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
