using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class DevTools {

    private string[] AllLines;

    private Dictionary<string, string> EnvironmentVariables = new Dictionary<string, string>();
    private Dictionary<string, bool> FlagDict = new Dictionary<string, bool>();
    private Dictionary<string, string> Paths = new Dictionary<string, string>();

    public DevTools()
    {
        Init();
    }

    private void Init()
    {
        string PATH = Application.streamingAssetsPath + "/.config";
        if (File.Exists(PATH)) {
            Debug.Log("[DEVTOOLS]: Configuration Found");
            AllLines = File.ReadAllLines(PATH);

            for (int i = 0; i < AllLines.Length; i++) {
                string[] args = AllLines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if(args.Length > 0) {
                    if (args[0] == "flag") {
                        FlagDict.Add(args[1], Convert.ToBoolean(args[2]));
                    }
                    else if (args[0] == "envar") {
                        EnvironmentVariables.Add(args[1], args[2]);
                    }
                    else if (args[0] == "path") {
                        string path_ = Regex.Replace(args[2], "PERSISTENT", SimpleSerializer.PERSISTENT);
                        Paths.Add(args[1], path_);
                    }
                }

            }
        }
    }

    public string GetEnvar(string varName)
    {
        if (EnvironmentVariables.ContainsKey(varName)) {
            return EnvironmentVariables[varName];
        }
        else return "";
    }

    public string GetPath(string pathName)
    {
        return Paths[pathName];
    }

    public bool this[string flag] {
        get {
            if (FlagDict.ContainsKey(flag)) {
                return FlagDict[flag];
            }
            else return false;
        }
    }

}
