using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class SnapToGrid : Editor {

    

    public override void OnInspectorGUI()
    {
        Transform trans = (Transform)target;

        base.OnInspectorGUI();

        float X = trans.position.x;
        float Y = trans.position.y;

        if (GUILayout.Button("snap")) {

            trans.position = new Vector2(Mathf.Round(X), Mathf.Round(Y));
        }

    }


}
