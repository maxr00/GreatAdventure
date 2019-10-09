using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ModularBuildingPiece))]
[CanEditMultipleObjects]
public class ModularMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ModularBuildingPiece modular = target as ModularBuildingPiece;

        if (!modular.locked)
            base.OnInspectorGUI();
        else if (GUILayout.Button("Unlock Mesh"))
        {
            modular.locked = false;
            base.OnInspectorGUI();
        }
        else
        {
            EditorGUILayout.Vector3IntField("Repeats (Preview)", modular.repeats);
        }
    }

    public void OnSceneGUI()
    {
        ModularBuildingPiece t = (target as ModularBuildingPiece);

        if(!t.locked)
        {
            EditorGUI.BeginChangeCheck();

            float size = HandleUtility.GetHandleSize(t.transform.position);
            var scale = Handles.ScaleHandle(Vector3.one + t.repeats, t.transform.position, t.transform.rotation, size * 1.5f);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change Repeats Value");
                t.repeats = new Vector3Int((int)scale.x, (int)scale.y, (int)scale.z) - Vector3Int.one;
                t.Update(); 
            }
        }

    }

}
