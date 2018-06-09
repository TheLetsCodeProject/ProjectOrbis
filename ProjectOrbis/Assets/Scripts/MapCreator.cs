using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

    private Dictionary<Color, GameObject> objectDictionary = new Dictionary<Color, GameObject>();

    public Texture2D level;
    public GameObject MissingTexture;

    public ObjectColorPair[] pairs = new ObjectColorPair[1];


    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < pairs.Length; i++) {
            objectDictionary.Add(pairs[i].Key, pairs[i].tile);


        }

    }

    private void Start()
    {
        int width = level.width;
        int height = level.height;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                Color col = level.GetPixel(x, y);

                if (objectDictionary.ContainsKey(col)) {
                    GameObject go = Instantiate(objectDictionary[col], new Vector2(x, y), Quaternion.identity, transform);
                    go.name = string.Format("tile ({0}, {1})", x, y);
                }
                else {
                    GameObject go = Instantiate(MissingTexture, new Vector2(x, y), Quaternion.identity, transform);
                    go.name = string.Format("tile ({0}, {1})", x, y);
                }





            }


        }
    }


}


[System.Serializable]
public struct ObjectColorPair
{
    public Color Key;
    [Header("GameObject To Spawn")]
    public GameObject tile;
    

}
