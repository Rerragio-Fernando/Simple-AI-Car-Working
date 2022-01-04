using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Checkpoint))]
public class CheckpointEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Checkpoint script = (Checkpoint)target;

        GUI.backgroundColor = Color.yellow;
        if(GUILayout.Button("Angle Size Checkpoint")){
            script.AnglesSizeCheckPointWalls();
        }
        
    }
}
