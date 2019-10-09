using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemAsset : ScriptableObject
{
    [MenuItem("Assets/Create/Item Asset")]
    public static void CreateAsset()
    {
        Item item_asset = CreateInstance<Item>();
        string asset_path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (asset_path == "")
        {
            asset_path = "Assets/Items";
        }

        asset_path = AssetDatabase.GenerateUniqueAssetPath(asset_path + "/New" + typeof(ItemAsset).ToString() + ".asset");
        Debug.Log(asset_path);
        AssetDatabase.CreateAsset(item_asset, asset_path);

        AssetDatabase.SaveAssets();
        Selection.activeObject = item_asset;
    }
}
