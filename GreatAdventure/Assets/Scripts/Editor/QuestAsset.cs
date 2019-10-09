using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuestAsset : ScriptableObject
{
    [MenuItem("Assets/Create/Quest Asset")]
    public static void CreateAsset()
    {
        Quest quest_asset = CreateInstance<Quest>();
        string asset_path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (asset_path == "")
        {
            asset_path = "Assets/Quests";
        }

        asset_path = AssetDatabase.GenerateUniqueAssetPath(asset_path + "/New" + typeof(QuestAsset).ToString() + ".asset");
        Debug.Log(asset_path);
        AssetDatabase.CreateAsset(quest_asset, asset_path);

        AssetDatabase.SaveAssets();
        Selection.activeObject = quest_asset;
    }
}
