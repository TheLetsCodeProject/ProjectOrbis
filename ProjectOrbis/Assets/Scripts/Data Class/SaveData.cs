using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public float SaveTime;
    public Vector2 Offset;
    
    public SaveData(float time, Vector2 offset){
        this.SaveTime = time;
        this.Offset = offset;
    }
       
}
