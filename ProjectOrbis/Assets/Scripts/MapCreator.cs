using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

    public GameObject crate;
    public Texture2D level;
    public GameObject metalFloor;
    public GameObject MissingTexture;

	// Use this for initialization
	void Awake () {

        int width = level.width;
        int height = level.height;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                Color col = level.GetPixel(x, y);

                if (col == Color.black) {
                    Instantiate(crate, new Vector2(x, y), Quaternion.identity);


                }
                else if (col == Color.white) {
                    Instantiate(metalFloor, new Vector2(x, y), Quaternion.identity);


                }
                else {
                    Instantiate(MissingTexture, new Vector2(x, y), Quaternion.identity);
                
                    
                }

            }
              

        }
	}
	
	
}
