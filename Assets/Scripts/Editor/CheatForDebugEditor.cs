using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CheatForDebug))]
public class CheatForDebugEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CheatForDebug script = (CheatForDebug)target;

        if(GUILayout.Button("Spawn Item Here"))
        {
            script.SpawItem();
        }
    }
}
