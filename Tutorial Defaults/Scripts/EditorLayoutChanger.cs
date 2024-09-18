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
            EditorUtility.FocusProjectWindow();
            var folder = AssetDatabase.LoadAssetAtPath<Object>(path);
            Selection.activeObject = folder;
            Debug.Log($"Switched Project view to folder: {path}");
        }
        else
        {
            Debug.LogError($"Invalid folder path: {path}");
        }
    }

    public static void FocusOnPrefabAsset(string path)
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab != null)
        {
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
        var allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (var obj in allObjects)
        {
            if (obj.name == name)
            {
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
        var hierarchyWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        if (hierarchyWindowType != null)
        {
            var hierarchyWindow = EditorWindow.GetWindow(hierarchyWindowType);
            if (hierarchyWindow != null)
            {
                var searchableWindow = typeof(SearchableEditorWindow);
                var setSearchMethod =
                    searchableWindow.GetMethod("SetSearch", BindingFlags.Instance | BindingFlags.NonPublic);

                if (setSearchMethod != null)
                {
                    setSearchMethod.Invoke(hierarchyWindow, new object[] {""});
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