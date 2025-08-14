using UnityEditor;
using UnityEngine;

public class OpenUIHandler
{
    [MenuItem("Tools/Open UI prefab")]
    public static void OpenMainMenuPrefab()
    {
        string path = "Assets/Resources/UIHandler.prefab";
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

        if (prefab == null)
        {
            Debug.LogError("Prefab not found at path: " + path);
            return;
        }

        // �������� ������ � Project Window
        Selection.activeObject = prefab;

        // ������� � Prefab Mode
        AssetDatabase.OpenAsset(prefab);
    }
}
