using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{

    [Header("Panel Parts")]
    public Text LevelName;
    public Text Difficulty;
    public Image Logo;

    [Header("Color Pallette")]
    public Color Easy;
    public Color Medium;
    public Color Hard;

    private LevelAsset myLevel;
    //Property call back pattern - Whever we change this panels
    //level asset, also update all its graphics.
    public LevelAsset Level {
        get {
            return myLevel;
        }
        set {
            myLevel = value;
            InitPanel(value);
        }
    }

    //Initialises panel using LevelAsset
    private void InitPanel(LevelAsset _level)
    {
        LevelName.text = _level.LevelName;
        
        //Black magic - The ternery operator is a condensed form of 'if' statement
        //If you wanna find out more google "C# ternery"
        Difficulty.text = (_level.Difficulty == DifficultyLevel.Easy ? "Easy" : 
                           _level.Difficulty == DifficultyLevel.Medium ? "Medium" : "Hard");

        //Sets panel color based on level difficulty
        if (Difficulty.text == "Easy") {
            gameObject.GetComponent<Image>().color = Easy;
        } else if (Difficulty.text == "Medium") {
            gameObject.GetComponent<Image>().color = Medium;
        } else if (Difficulty.text == "Hard") {
            gameObject.GetComponent<Image>().color = Hard;
        } else { //If there are something horrible has happened and there is no level
                 // Difficulty default to gray.
            gameObject.GetComponent<Image>().color = Color.gray;
        }
    }


}
