using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour {

	public void LoadScene(string name)
    {
        GameManager.ins.LoadScene(name);
    }
        
}
