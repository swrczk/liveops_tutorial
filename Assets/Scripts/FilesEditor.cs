using System.IO;
using UnityEditor;
using UnityEngine;


public class FilesEditor : MonoBehaviour
{
    public static void DeleteFileFromProject(string relativePath)
    {
        // Ścieżka do pliku w folderze Assets
        string fullPath = Path.Combine(Application.dataPath, relativePath);

        // Sprawdzenie, czy plik istnieje
        if (File.Exists(fullPath))
        {
            // Usunięcie pliku
            FileUtil.DeleteFileOrDirectory(fullPath);

            // Odśwież AssetDatabase, aby Unity zaktualizowało zawartość projektu
            AssetDatabase.Refresh();

            Debug.Log($"File '{relativePath}' has been deleted.");
        }
        else
        {
            Debug.LogWarning($"File '{relativePath}' not found.");
        }
    }
    public static bool IsFileInProject(string relativePath)
    {
        // Użyj AssetDatabase, aby sprawdzić, czy plik istnieje
        string fullPath = "Assets/" + relativePath;
        return AssetDatabase.LoadAssetAtPath<Object>(fullPath) != null;
    }

}