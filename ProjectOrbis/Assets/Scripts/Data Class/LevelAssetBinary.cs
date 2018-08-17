using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[Serializable]
public class LevelAssetBINARY
{
    [XmlElement("Level Name")]
    public string name;
    [XmlElement("Level Difficulty")]
    public int difficulty;
    public string PublicKey;
    public string PrivateKey;
    public string Savekey;
    [XmlElement("map data")]
    public byte[] image;

    public LevelAsset Convert()
    {
        LevelAsset level = ScriptableObject.CreateInstance<LevelAsset>();

        level.LevelName = name;
        level.Difficulty = (DifficultyLevel)difficulty;
        level.PublicKey = PublicKey;
        level.PrivateKey = PrivateKey;
        level.SaveKey = Savekey;
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(image);
        level.LevelTexture = tex;
        return level;
    }
}
