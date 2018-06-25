using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    LevelAsset[] levels;
    public GameObject PanelPrefab;
    public GameObject ListParent;
    // Use this for initialization
    void Start()
    {
        levels = GameManager.ins.levels;

        for (int i = 0; i < levels.Length; i++) {

            GameObject currentPanel = Instantiate(PanelPrefab, ListParent.transform);
            currentPanel.GetComponent<Panel>().Level = levels[i];
        }
    }
}
