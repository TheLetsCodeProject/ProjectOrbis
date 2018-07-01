using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dreamlo/Dreamlo Client")]
public class Dreamlo : MonoBehaviour {

    [Header("Keys:")]
    [Tooltip("The public key of your project on dreamlo")]
    public string PublicKey;
    [Tooltip("The public key of your project on dreamlo")]
    public string PrivateKey;
    
    //The address of dreamlo
    const string ServiceAddress = "www.dreamlo.com/lb/";

    //The callback for returning our scores to after download is complete
    public Action<List<ScoreData>> CallBack;
	

    public void AddScore(string username, int seconds)
    {
        StartCoroutine(UploadScore(username, seconds));
    }

    public void DownloadScores()
    {
        StartCoroutine(DownloadScoreData());
    }

    IEnumerator UploadScore(string _username, int _seconds)
    {
        WWW www = new WWW(ServiceAddress + PrivateKey + "/add/" + WWW.EscapeURL(_username) + "/0/" + _seconds);
        yield return www;

        if (!string.IsNullOrEmpty(www.error)) {
            Debug.LogError(www.error);
        }
    }

    IEnumerator DownloadScoreData ()
    {
        WWW www = new WWW(ServiceAddress + PublicKey + "/pipe-seconds-asc");
        yield return www;

        if (!string.IsNullOrEmpty(www.error)) {
            Debug.LogError(www.error);
        } else {
            FormatData(www.text);
        }
    }

    void FormatData(string data)
    {
        //Gets each score
        string[] entries = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        List<ScoreData> scoreDatas = new List<ScoreData>();

        for (int i = 0; i < entries.Length; i++) {

            string[] items = entries[i].Split(new char[] { '|' });

            var _username = items[0];
            var _time = float.Parse(items[2]);
            ScoreData score = new ScoreData(_username, _time);

            scoreDatas.Add(score);
        }

        if (CallBack != null) {
            CallBack(scoreDatas);
        }
    }
}

public struct ScoreData {
    public string Username;
    public float Seconds;

    public ScoreData(string _username, float _seconds)
    {
        Username = WWW.UnEscapeURL(_username);
        Seconds = _seconds;
    }
}
