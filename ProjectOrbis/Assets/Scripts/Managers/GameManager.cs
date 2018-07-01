using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbis.Timing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public TimeData lastTime;

    #region Properties
    [SerializeField]
    private LevelAsset m_LevelToLoad;
    public LevelAsset LevelToLoad { get { return m_LevelToLoad; } }

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
    public void LoadLevel(LevelAsset level)
    {
        if (level == null || level.LevelTexture == null) {
            Debug.LogError("Incomplete level asset: " + level.name);
            return;
        }

        m_LevelToLoad = level;
        SceneManager.LoadScene("LevelScene");
    }
    #endregion

}


