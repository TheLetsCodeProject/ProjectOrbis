using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        GameManager myGM = (GameManager)target;
        SerializedProperty prop = serializedObject.FindProperty("levels");
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        //If the plus button is pressed, increase the array size in the inspector
        if (GUILayout.Button("+")) {
            prop.arraySize += 1;
            serializedObject.ApplyModifiedProperties(); //Applies the increased size
        }
        //If the minus button is pressed, decrease the array size in the inspector
        if (GUILayout.Button("-")) {
            prop.arraySize -= 1;
            serializedObject.ApplyModifiedProperties(); //Applies the decreased size
        }
        GUILayout.EndHorizontal();
    }
}
