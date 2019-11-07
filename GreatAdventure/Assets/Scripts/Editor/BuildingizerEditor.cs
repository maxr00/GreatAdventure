using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Buildingizer))]
[CanEditMultipleObjects]
public class BuildingizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(15);

        if (GUILayout.Button("BUILDINGIZE"))
        {
            foreach(var t in targets)
            {
                (t as Buildingizer).Buildingize();
            }
        }

        GUILayout.Space(15);

        base.OnInspectorGUI();

        
    }

}
