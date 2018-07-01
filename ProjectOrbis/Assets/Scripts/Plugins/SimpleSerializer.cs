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


}
