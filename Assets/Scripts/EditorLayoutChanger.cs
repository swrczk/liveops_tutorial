#if UNITY_EDITOR

using System.Reflection;
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
    public static void ClearHierarchySearchField()
    {
        // Pobierz okno Hierarchii za pomocą refleksji
        var hierarchyWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        if (hierarchyWindowType != null)
        {
            // Znajdź otwarte okno hierarchii
            var hierarchyWindow = EditorWindow.GetWindow(hierarchyWindowType);
            if (hierarchyWindow != null)
            {
                // Użyj refleksji, aby uzyskać dostęp do pola wyszukiwania i wyczyścić je
                var searchableWindow = typeof(SearchableEditorWindow);
                var setSearchMethod = searchableWindow.GetMethod("SetSearch", BindingFlags.Instance | BindingFlags.NonPublic);
                
                if (setSearchMethod != null)
                {
                    // Ustaw pustą wartość wyszukiwania
                    setSearchMethod.Invoke(hierarchyWindow, new object[] { "" });
                    Debug.Log("Hierarchy search field cleared.");
                }
                else
                {
                    Debug.LogError("Unable to find SetSearch method.");
                }
            }
        }
        else
        {
            Debug.LogError("Unable to find SceneHierarchyWindow type.");
        }
    }
}
#endif