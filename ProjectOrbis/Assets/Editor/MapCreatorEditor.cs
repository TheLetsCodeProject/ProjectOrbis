using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Orbis.Data;
using System.IO;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorEditor : Editor {

    //Overrides default inspector
    public override void OnInspectorGUI()
    {
        //Gets a refernce to the 'MapCreator' instance that we are inspecting.
        MapCreator myCreator = (MapCreator)target;
        
        //Gets a refernce to the editor representation of the 'pairs' array
        //which can be found in the MapCreator class.
        SerializedProperty prop = serializedObject.FindProperty("pairs");
        //Displays Normal GUI
        base.OnInspectorGUI();

        //Simply starts a limited area, anything inside this will be placed in one
        //horizontal line.
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
        GUILayout.EndHorizontal(); //End the horizontal layout group

        GUILayout.BeginHorizontal(); //New horizontal layout group below the last one
        GUILayout.Label("Save list as:", EditorStyles.boldLabel);

        if (GUILayout.Button(".txt")) {
            GenerateTextRef(myCreator.pairs); //If the txt option is pressed, save a text file
        } 
        if (GUILayout.Button(".css")) {
            GenerateCSSRef(myCreator.pairs);  //If the css option is pressed, save a css file
        }

        GUILayout.EndHorizontal(); //Ends horizontal
    }

    //This function formats a file with refernces to color and block
    void GenerateTextRef(ObjectColorPair[] array)
    {
        //Initialise file content
        string DocString = string.Empty;

        //Loop through every ObjectColorPair in pairs array
        for (int i = 0; i < array.Length; i++) {
            ObjectColorPair o = array[i]; //Stores current item in array

            //Does some black magic in order to format ObjectColorPair to look like
            //0. BlockName == (255, 55, 66)
            DocString += string.Format("{0}. {1} == ({2}, {3}, {4})",
                i, o.Name, o.Key.r * 255, o.Key.g * 255, o.Key.b * 255);
            DocString += "\n";
        }

        //Opens a save file dialog
        string Path = EditorUtility.SaveFilePanel("Save text ref", "", "Color reference.txt", "txt");

        //if the path is empty of has been cancelled, return and do nothing
        if (Path == string.Empty) {
            return;
        }
        File.WriteAllText(Path, DocString); //Write formatted text to file path

    }

    //Formats ObjectcolorPairs as Photoshop readable css file
    void GenerateCSSRef(ObjectColorPair[] array)
    {
        string DocString = string.Empty;  //Initialises css contents

        //Loop through every ObjectColorPair in pairs array
        for (int i = 0; i < array.Length; i++) {
            ObjectColorPair o = array[i];  //Stores current item in array

            //Formats current ObjectcolorPair as a css 'a' tag with color content.
            // a{
            //    color: rgb(255, 55, 0);
            // }
            DocString += "a {";
            DocString += string.Format("color: rgb({0},{1},{2});", 
                Mathf.Round(o.Key.r * 255), 
                Mathf.Round(o.Key.g * 255), 
                Mathf.Round(o.Key.b * 255));
            DocString += "}";
        }

        //opens save file dialog
        string Path = EditorUtility.SaveFilePanel("Save CSS swatches", "", "Color reference.css", "css");
        //Checks for null path
        if (Path == string.Empty) {
            return;
        }
        File.WriteAllText(Path, DocString); //Writes data.
    }
}
