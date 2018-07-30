using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelAsset))]
public class LevelAssetEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate key"))
        {
            ((LevelAsset)target).SaveKey = RandomKey();
        }

        if (GUILayout.Button("Reset level save"))
        {
            SimpleSerializer.ClearKey(((LevelAsset)target).SaveKey);
        }
    }

    public string RandomKey()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[15];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new string(stringChars);
        return finalString;
    }

}
