﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbis.Data;


public class MapCreator : MonoBehaviour {

    //Creates our prefab dictionary
    private Dictionary<Color, GameObject> objectDictionary = new Dictionary<Color, GameObject>();
    private LevelAsset currentLevel;
    private GameObject Player;
    private GameObject PlayerCopy;
    private Vector2 spawnPos;
    GameObject SpawnNode;

    [Header("Preferences")]
    public bool DoBreakOnError = false; //Does the user want an editor break on error
    [Space(10)]
    [Header("Level Objects")]
    public GameObject EmptySpawn;
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

        //TODO: WTF is this! The map creator class should probably be a service clas
        //      that could have a level object passes to it along with a world object
        //      to attach the necessary objects to
        currentLevel = GameManager.ins.LevelToLoad;
        Player = GameManager.ins.Player;

    }

    private void Start()
    {
        #region Level Generation
        int width = currentLevel.LevelTexture.width;
        int height = currentLevel.LevelTexture.height;
        
        //Loops through all pixels in level texture
        //TODO: Gut this entire function
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                Color col = currentLevel.LevelTexture.GetPixel(x, y);

                if (objectDictionary.ContainsKey(col)) {
                    GameObject go = Instantiate(objectDictionary[col], new Vector2(x, y), Quaternion.identity, transform);
                    go.name += string.Format(" ({0}, {1})", x, y);
                }
                else {
                    GameObject go = Instantiate(MissingTexture, new Vector2(x, y), Quaternion.identity, transform);
                    go.name += string.Format("MissingTexture ({0}, {1})", x, y);
                    Debug.LogWarning(string.Format("Tile could not be found (tile at {0}, {1})", x, y) + string.Format("R: {0} G: {1} B: {2}", col.r * 255, col.g * 255, col.b * 255));
                  
                    if (DoBreakOnError) { // a setting flag like -gl_hard_fix would be better
                        
                        Debug.Break();
                    }                
                }
            }
        }
        #endregion

        SpawnNode = GameObject.FindGameObjectWithTag("SpawnNode");
        if(SpawnNode == null) {
            Debug.LogError("No spawn node was found, have you forgotten to add one");
            return;
        }    
        SpawnPlayer();
    }
    
    private void SpawnPlayer() {
        spawnPos = SpawnNode.transform.position + new Vector3(0.5f, 0.5f);
        Vector2 offset = spawnPos + currentLevel.LevelData.Offset;
        Instantiate(EmptySpawn, offset, Quaternion.identity);
        PlayerCopy = Instantiate(Player, offset, Quaternion.identity);
        LevelCamera.gameObject.SetActive(false);
    }

    public void LoadScene(string name)
    {
        GameManager.ins.LoadScene(name);
    }

    //TODO: Move this shit somewhere else. SRP is a scared pact
    public void SaveGame() {
        Debug.Log("Saved");
        if (GameManager.ins.LevelTimer.IsStarted) {

            Vector2 PlayerOffset = ((Vector2)PlayerCopy.transform.position) - spawnPos;
            float LastTime = GameManager.ins.LevelTimer.GetCurrentTime();

            SimpleSerializer.SaveVector(currentLevel.SaveKey, PlayerOffset);
            SimpleSerializer.SaveFloat(currentLevel.SaveKey, LastTime);
        }
    }

    public void ResetLevel()
    {
        GameManager.ins.LevelTimer.Reset();
        GameManager.ins.ResetData(currentLevel);
        GameManager.ins.LoadLevel(currentLevel);
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
