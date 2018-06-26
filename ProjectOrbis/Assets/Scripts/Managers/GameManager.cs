using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbis.Timing;

public class GameManager : MonoBehaviour {

    public TimeData lastTime;

    #region Properties
    [SerializeField]
    private Texture2D m_MapToLoad;
    public Texture2D MapToLoad { get { return m_MapToLoad; } }

    [SerializeField]
    private GameObject m_Player;
    public GameObject Player { get { return m_Player; } }
    public LevelAsset[] levels;
    #endregion

    #region Members
    Timer LevelTimer = new Timer();


    #endregion

    public static GameManager ins;

    private void Awake()
    {
        if (ins == null) {
            ins = this;
        } else {
            Destroy(this);
            Debug.Log("Deleted existing GameManager");

        }

        DontDestroyOnLoad(this.gameObject);
    }


    #region Logic

    public void StartGame()
    {
        Debug.Log("Map Started");
        LevelTimer.Start();
        
    }

    public void EndGame()
    {
        Debug.Log("Map Completed");
        if (LevelTimer.IsStarted) {
            LevelTimer.Stop();
            lastTime = LevelTimer.GetFormattedTime();
        }
    }
    #endregion

}


