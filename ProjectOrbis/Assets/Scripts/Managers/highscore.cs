using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Orbis.Timing;

public class highscore : MonoBehaviour {

    Dreamlo scoreboard;
    public Text[] Cards = new Text[10];

    // Update is called once per frame
	void Awake() {
        scoreboard = GetComponent<Dreamlo>();
        scoreboard.PublicKey = GameManager.ins.LevelToLoad.PublicKey;
        scoreboard.PrivateKey = GameManager.ins.LevelToLoad.PrivateKey;
        for (int i = 0; i < Cards.Length; i++) {
            Cards[i].text = "Loading...";

        }
		
	}

	// Use this for initialization
	void Start () {

        
        scoreboard.CallBack += DisplayHighscores;
        scoreboard.AddScore(GameManager.ins.Username, (int)GameManager.ins.lastTime.SecondsRaw);
        scoreboard.DownloadScores();
        StartCoroutine("RefreshScore");
		
	}
	
    IEnumerator RefreshScore()
    {
        while(true) {
            scoreboard.DownloadScores();
            yield return new WaitForSeconds(5f);
        }
    }
	

    public void DisplayHighscores(List<ScoreData> data)
    {
        Debug.Log("lmao");
        for(int i = 0; i < Cards.Length; i++) {
            if(i >= data.Count) {
                Cards[i].text = "";
            } else {
                string time = Timer.FormatTime(data[i].Seconds, "00", "00");
                Cards[i].text = data[i].Username + " - " + time;
            }

        }

    }
}
