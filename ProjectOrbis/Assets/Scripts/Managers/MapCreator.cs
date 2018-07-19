using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbis.Data;


public class MapCreator : MonoBehaviour {

    //Creates our prefab dictionary
    private Dictionary<Color, GameObject> objectDictionary = new Dictionary<Color, GameObject>();
    private LevelAsset level;
    private GameObject Player;
    private GameObject PlayerCopy;
    private Vector2 spawnPos;
    GameObject[] SpawnNodes;

    [Header("Preferences")]
    public bool DoBreakOnError = false; //Does the user want an editor break on error
    [Space(10)]
    [Header("Level Settings")]
    public GameObject MissingTexture;
    public Camera LevelCamera;

    [Space(10)]

    [Header("Object-Color Dictionary")]
    //The array so that we can set values in inspector
    public ObjectColorPair[] pairs = new ObjectColorPair[1];

    // Use this for initialization
    void Awake()
    {
        //Parses our inspector list to a dictionary
        for (int i = 0; i < pairs.Length; i++) {
            pairs[i].tile.name = pairs[i].Name;
            objectDictionary.Add(pairs[i].Key, pairs[i].tile);
        }

        level = GameManager.ins.LevelToLoad;
        Player = GameManager.ins.Player;

    }

    private void Start()
    {
        #region Level Generation
        int width = level.LevelTexture.width;
        int height = level.LevelTexture.height;
        
        //Loops through all pixels in level texture
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                Color col = level.LevelTexture.GetPixel(x, y);

                if (objectDictionary.ContainsKey(col)) {
                    GameObject go = Instantiate(objectDictionary[col], new Vector2(x, y), Quaternion.identity, transform);
                    go.name += string.Format(" ({0}, {1})", x, y);
                }
                else {
                    GameObject go = Instantiate(MissingTexture, new Vector2(x, y), Quaternion.identity, transform);
                    go.name += string.Format("MissingTexture ({0}, {1})", x, y);
                    Debug.LogWarning(string.Format("Tile could not be found (tile at {0}, {1})", x, y) + string.Format("R: {0} G: {1} B: {2}", col.r * 255, col.g * 255, col.b * 255));
                  
                    if (DoBreakOnError) {
                        Debug.Break();
                    }                
                }
            }
        }
        #endregion

        SpawnNodes = GameObject.FindGameObjectsWithTag("SpawnNode");
        if(SpawnNodes.Length == 0) {
            Debug.LogError("No spawn node was found, have you forgotten to add one");
            return;
        }    
        SpawnPlayer();
    }
    
    private void SpawnPlayer() {
        int Index = Random.Range(0, SpawnNodes.Length);
        spawnPos = SpawnNodes[Index].transform.position + new Vector3(0.5f, 0.5f);
        PlayerCopy = Instantiate(Player, spawnPos + level.LevelData.Offset, Quaternion.identity);
        LevelCamera.gameObject.SetActive(false);
    }

    public void LoadScene(string name)
    {
        GameManager.ins.LoadScene(name);
    }

    public void SaveGame() {
        Debug.Log("called");
        if (GameManager.ins.LevelTimer.IsStarted) {
            SimpleSerializer.SaveVector(level.SaveKey, ((Vector2)PlayerCopy.transform.position).Minus(spawnPos));
            SimpleSerializer.SaveFloat(level.SaveKey, GameManager.ins.LevelTimer.GetCurrentTime());
        }
    
    }

    private void Update()
    {
        if (GameManager.ins.LevelTimer.IsStarted == false && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)) {
            GameManager.ins.StartGame();
        }
    }
}

namespace Orbis { namespace Data {

        [System.Serializable]
        public struct ObjectColorPair
        {
            public string Name;
            [Header("Object-Color Pair")]
            public Color Key;
            [Tooltip("The gameobject to spawn")]
            public GameObject tile;
        }

    }
}
