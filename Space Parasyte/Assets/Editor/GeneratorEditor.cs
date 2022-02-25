using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class GeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RoomManager roomManager = (RoomManager)target;

        if(GUILayout.Button("Reset Levels"))
        {
            roomManager.ResetLevels();
        }

    }

}
