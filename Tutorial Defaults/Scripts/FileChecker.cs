using UnityEngine;
using UnityEditor;

public class FileChecker : MonoBehaviour
{
    // Sprawdza, czy plik o podanej ścieżce istnieje w projekcie
    public static bool IsFileInProject(string relativePath)
    {
        // Użyj AssetDatabase, aby sprawdzić, czy plik istnieje
        string fullPath = "Assets/" + relativePath;
        return AssetDatabase.LoadAssetAtPath<Object>(fullPath) != null;
    }
}