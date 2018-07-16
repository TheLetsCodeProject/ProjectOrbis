using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Compression;
using System.IO;
using System.Xml.Serialization;

public static class LevelFactory {

    public static List<LevelAsset> ConstructFromFolderCONFIG(string path)
    {
        Debug.LogWarning("ConstructFromFolderCONFIG is deprecated, please use ConstructFromFolder instead");

        string[] Levels = Directory.GetFiles(path, "*.config");
        List<LevelAsset> levelAssets = new List<LevelAsset>();

        for (int i = 0; i < Levels.Length; i++) {
            string[] content = File.ReadAllLines(Levels[i]);
            string imagePath = content[2];

            byte[] imageData = File.ReadAllBytes(Environment.GetPath("demo") + "/" + imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            LevelAsset level = ScriptableObject.CreateInstance<LevelAsset>();
            level.LevelName = content[0];
            level.Difficulty = (DifficultyLevel)int.Parse(content[1]);
            level.LevelTexture = texture;

            levelAssets.Add(level);
            Debug.Log("Created Level");
        }

        return levelAssets;
    }

    public static List<LevelAsset> ConstructFromFolder(string PATH)
    {
        string[] levelPaths = Directory.GetFiles(PATH, "*.level");
        List<LevelAsset> levels = new List<LevelAsset>();

        foreach (string filePath in levelPaths) {
            levels.Add(ReadLevel(filePath).Convert());
        }

        return levels;
    }

    public static void WriteLevel(string PATH, LevelAssetBINARY level)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelAssetBINARY));

        using (Stream stream = new FileStream(PATH, FileMode.Create, FileAccess.Write, FileShare.None)) {
            serializer.Serialize(stream, level);
        }
    }

    public static LevelAssetBINARY ReadLevel(string PATH)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelAssetBINARY));

        using (Stream stream = new FileStream(PATH, FileMode.Open, FileAccess.Read, FileShare.Read)) {
            LevelAssetBINARY level = (LevelAssetBINARY)serializer.Deserialize(stream);
            return level;
        }
    }

    public static byte[] GetImageBytes(string PATH)
    {
        return File.ReadAllBytes(PATH);
    }
}
