using UnityEditor;
using UnityEngine;

public class EditorLayoutChanger : MonoBehaviour
{
    public static void SwitchProjectFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path))
        {
            // Otwiera dany folder w widoku projektu
            EditorUtility.FocusProjectWindow();
            Object folder = AssetDatabase.LoadAssetAtPath<Object>(path);
            Selection.activeObject = folder;
            Debug.Log($"Switched Project view to folder: {path}");
        }
        else
        {
            Debug.LogError($"Invalid folder path: {path}");
        }
    }
    
    
    // Funkcja przełączająca widok projektu na prefab
    public static  void FocusOnPrefabAsset(string path)
    {
        // Sprawdza, czy ścieżka jest poprawna
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab != null)
        {
            // Skupia widok projektu na prefabu
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = prefab;
            Debug.Log($"Focused on prefab: {path}");
        }
        else
        {
            Debug.LogError($"Invalid prefab path: {path}");
        }
    }
}