using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadAsset : ScriptableObject
{
    [MenuItem("Assets/Create/Load Asset")]
    public static void CreateAsset()
    {
        LoadData load_asset = CreateInstance<LoadData>();
        string asset_path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (asset_path == "")
        {
            asset_path = "Assets/";
        }

        asset_path = AssetDatabase.GenerateUniqueAssetPath(asset_path + "/New" + typeof(LoadAsset).ToString() + ".asset");
        Debug.Log(asset_path);
        AssetDatabase.CreateAsset(load_asset, asset_path);

        AssetDatabase.SaveAssets();
        Selection.activeObject = load_asset;
    }
}
