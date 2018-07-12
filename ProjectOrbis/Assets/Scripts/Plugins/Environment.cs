using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class Environment {

    public static Environment ins;
    private string[] AllLines;

    private Dictionary<string, string> EnvironmentVariables = new Dictionary<string, string>();
    private Dictionary<string, bool> Flags = new Dictionary<string, bool>();
    private Dictionary<string, string> Paths = new Dictionary<string, string>();

    private Dictionary<string, string> Keywords =
    new Dictionary<string, string>()
    {
        {"ROOT", SimpleSerializer.ROOT },
        {"PERSISTENT", SimpleSerializer.PERSISTENT },
        {"SAVE", SimpleSerializer.SAVE }
    };

    public Environment(string filename = ".config")
    {
        Init(filename);
        ins = this;
    }

    private void Init(string filename)
    {
        string PATH = Application.streamingAssetsPath + "/" + filename;

        if (File.Exists(PATH)) {

            Debug.Log("[DEVTOOLS]: Configuration Found");
            AllLines = File.ReadAllLines(PATH);

            for (int i = 0; i < AllLines.Length; i++) {

                string[] args = AllLines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (args.Length > 0) {

                    string classifier = args[0];
                    string name = args[1];
                    string value = args[2];

                    if (classifier == "flag") {
                        Flags.Add(name, Convert.ToBoolean(value));
                    }

                    else if (classifier == "envar") {
                        EnvironmentVariables.Add(name, value);
                    }

                    else if (classifier == "path") {
                        string path = value;

                        foreach (string key in Keywords.Keys) {
                            path = Regex.Replace(path, key, Keywords[key]);
                        }

                        Paths.Add(name, path);
                    }
                }
            }
        }
    }

    private EnvironmentData GetEnvarInternal(string varName)
    {
        if (EnvironmentVariables.ContainsKey(varName)) {
            return new EnvironmentData(EnvironmentVariables[varName]);
        }
        else return EnvironmentData.Empty;
    }

    private string GetPathInternal(string pathName)
    {
        return Paths[pathName];
    }

    public bool this[string flag] {
        get {
            if (Flags.ContainsKey(flag)) {
                return Flags[flag];
            }
            else return false;
        }
    }

    public static string GetPath(string pathName)
    {
        return ins.GetPathInternal(pathName);
    }

    public static bool GetFlag(string flagName)
    {
        return ins[flagName];
    }

    public static EnvironmentData GetEnvar(string varName)
    {
        return ins.GetEnvarInternal(varName);
    }
}

public class EnvironmentData {
    public string data;

    public EnvironmentData(string _data)
    {
        data = _data;
    }

    public static EnvironmentData Empty = new EnvironmentData("");

    public static implicit operator string(EnvironmentData d)
    {
        return d.data;
    }

    public static implicit operator bool(EnvironmentData d)
    {
        return Convert.ToBoolean(d.data);
    }

    public static implicit operator float(EnvironmentData d)
    {
        return float.Parse(d.data);
    }

    public static implicit operator int(EnvironmentData d)
    {
        return int.Parse(d.data);
    }
}

