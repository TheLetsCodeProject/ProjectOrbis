using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SimpleSerializer
{

    public static void SaveInt(string key, int value)
    {
        string PATH = Application.streamingAssetsPath + "/" + key + ".txt";
        File.WriteAllText(PATH, value.ToString());
    }

    public static int LoadInt(string key)
    {
        string PATH = Application.streamingAssetsPath + "/" + key + ".txt";
        string result = File.ReadAllText(PATH);
        return int.Parse(result);
    }

    public static void SaveVector(string key, Vector2 value) {

        string PATH = Application.streamingAssetsPath + "/" + key + ".txt";
        string[] data = new string[2];
        data[0] = value.x.ToString();
        data[1] = value.y.ToString();
        File.WriteAllLines(PATH, data);
    }

    public static Vector2 LoadVector(string key)
    {
        string PATH = Application.streamingAssetsPath + "/" + key + ".txt";
        string[] data = File.ReadAllLines(PATH);
        float x = float.Parse(data[0]);
        float y = float.Parse(data[1]);

        return new Vector2(x, y);
    }

    public static bool IsFirstLoad()
    {
        if (File.Exists(Application.streamingAssetsPath + "/log.q")) {

            return false;
        }
        else {

            File.Create(Application.streamingAssetsPath + "/log.q");

            return true;
        }

   
    }


}
