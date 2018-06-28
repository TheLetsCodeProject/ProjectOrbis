﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class LevelAsset : ScriptableObject {

    public string LevelName;

    [Tooltip("0: Easy, 1: Medium, 2: Hard")]
    public DifficultyLevel Difficulty;
    public Texture2D LevelTexture;

}

//Enums just give us a drop down menu in the editor while still acting like numbers.
public enum DifficultyLevel { Easy = 0, Medium = 1, Hard = 2, Extreme = 3}
