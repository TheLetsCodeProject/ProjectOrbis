﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbis.Data;


public class MapCreator : MonoBehaviour {

    //Creates our prefab dictionary
    private Dictionary<Color, GameObject> objectDictionary = new Dictionary<Color, GameObject>();

    [Header("Level Settings")]
    [Tooltip("The level texture to load")]
    public Texture2D level;
    public GameObject MissingTexture;
    public GameObject Player;
    public Camera LevelCamera;

    [Space(10)]

    [Header("Object-Color Dictionary")]
    //The array so that we can set values in inspector
    public ObjectColorPair[] pairs = new ObjectColorPair[1];

    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < pairs.Length; i++) {
            pairs[i].tile.name = pairs[i].Name;
            objectDictionary.Add(pairs[i].Key, pairs[i].tile);
        }

    }

    private void Start()
    {
        #region Level Generation
        int width = level.width;
        int height = level.height;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                Color col = level.GetPixel(x, y);

                if (objectDictionary.ContainsKey(col)) {
                    GameObject go = Instantiate(objectDictionary[col], new Vector2(x, y), Quaternion.identity, transform);
                    go.name += string.Format(" ({0}, {1})", x, y);
                }
                else {
                    GameObject go = Instantiate(MissingTexture, new Vector2(x, y), Quaternion.identity, transform);
                    go.name += string.Format(" ({0}, {1})", x, y);
                    Debug.LogWarning(string.Format("Tile could not be found (tile at {0}, {1})", x, y) + string.Format("R: {0} G: {1} B: {2}", col.r * 255, col.g * 255, col.b * 255));
                    Debug.Break();
                }

            }
        }
        #endregion

        GameObject[] SpawnNodes = GameObject.FindGameObjectsWithTag("SpawnNode");
        if(SpawnNodes.Length == 0) {
            Debug.LogError("No spawn node was found, have you forgotten to add one");
            return;
        }

        int Index = Random.Range(0, SpawnNodes.Length);
        Vector2 position = SpawnNodes[Index].transform.position + new Vector3(0.5f, 0.5f);
        Instantiate(Player, position, Quaternion.identity);

        LevelCamera.gameObject.SetActive(false);
    
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