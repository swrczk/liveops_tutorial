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
    public static void FocusOnPrefabAsset(string path)
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


    public static void FocusOnHierarchyObject(string name)
    {
        // Wyszukuje obiekt w hierarchii na podstawie jego nazwy
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                // Ustawia aktywny obiekt w hierarchii
                Selection.activeGameObject = obj;
                EditorGUIUtility.PingObject(obj);
                Debug.Log($"Focused on GameObject: {name}");
                return;
            }
        }

        Debug.LogError($"Object with name {name} not found in hierarchy.");
    }
}