using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelAsset : ScriptableObject {
    public string LevelName;
    [Range(0, 3)]
    public int Difficulty;
    public Texture2D LevelTexture;




}


