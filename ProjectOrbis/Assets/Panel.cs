using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{

    [Header("Panel Parts")]
    public Text LevelName;

    public string displayname { set { LevelName.text = value; } }



}
