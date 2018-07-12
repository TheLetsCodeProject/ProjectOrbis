using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Compression;
using System.IO;

public static class LevelFactory {

    public static List<LevelAsset> ConstructFromFolder(string path)
    {

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
}
