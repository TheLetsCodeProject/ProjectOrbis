using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour {

    public Text timerDisplay;

    private void Update()
    {
        timerDisplay.text = GameManager.ins.LevelTimer.GetCurrentTimeString();
    }
}
