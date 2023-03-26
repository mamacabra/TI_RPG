using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGeneratorTest))]
public class IslandCreator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapGeneratorTest myScript = (MapGeneratorTest)target;
        if(GUILayout.Button("Build Object"))
        {
            myScript.InstantiateIsland();
        }
        if (GUILayout.Button("Reset/Delete All"))
        {
            myScript.ResetAll();
        }
    }
    
   
}
