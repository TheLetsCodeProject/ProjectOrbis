using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Orbis.Data;
using System.IO;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorEditor : Editor {

    public override void OnInspectorGUI()
    {
        MapCreator myCreator = (MapCreator)target;
        SerializedProperty prop = serializedObject.FindProperty("pairs");
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("+")) {
            prop.arraySize += 1;
            serializedObject.ApplyModifiedProperties();
        }
        if (GUILayout.Button("-")) {
            prop.arraySize -= 1;
            serializedObject.ApplyModifiedProperties();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Save list as:", EditorStyles.boldLabel);
        if (GUILayout.Button(".txt")) {
            GenerateTextRef(myCreator.pairs);
        } 
        if (GUILayout.Button(".css")) {
            GenerateCSSRef(myCreator.pairs);
        }

        GUILayout.EndHorizontal();
    }

    void GenerateTextRef(ObjectColorPair[] array)
    {
        string DocString = string.Empty;
        for (int i = 0; i < array.Length; i++) {
            ObjectColorPair o = array[i];
            DocString += string.Format("{0}. {1} == ({2}, {3}, {4})",
                i, o.Name, o.Key.r * 255, o.Key.g * 255, o.Key.b * 255);
            DocString += "\n";
        }

        string Path = EditorUtility.SaveFilePanel("Save text ref", "", "Color reference.txt", "txt");
        if (Path == string.Empty) {
            return;
        }
        File.WriteAllText(Path, DocString);

    }

    void GenerateCSSRef(ObjectColorPair[] array)
    {
        string DocString = string.Empty;
        for (int i = 0; i < array.Length; i++) {
            ObjectColorPair o = array[i];
            DocString += "a {";
            DocString += string.Format("color: rgb({0},{1},{2});", 
                Mathf.Round(o.Key.r * 255), 
                Mathf.Round(o.Key.g * 255), 
                Mathf.Round(o.Key.b * 255));
            DocString += "}";
        }

        string Path = EditorUtility.SaveFilePanel("Save CSS swatches", "", "Color reference.css", "css");
        if (Path == string.Empty) {
            return;
        }
        File.WriteAllText(Path, DocString);
    }
}
