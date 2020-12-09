using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGen))]
public class LevelGenEditor : Editor
{
    override public void  OnInspectorGUI () {
        LevelGen LevelGen = (LevelGen) target;
        if (GUILayout.Button("Generate Level")) {
            LevelGen.GenerateLevel();
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
        DrawDefaultInspector();
    }
}
