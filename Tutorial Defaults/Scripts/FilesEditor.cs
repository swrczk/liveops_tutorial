#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class FilesEditor : MonoBehaviour
    {
        public static void DeleteFileFromProject(string relativePath)
        {
            var fullPath = Path.Combine(Application.dataPath, relativePath);
            if (File.Exists(fullPath))
            {
                FileUtil.DeleteFileOrDirectory(fullPath);
                AssetDatabase.Refresh();

                Debug.Log($"File '{relativePath}' has been deleted.");
            }
            else
            {
                Debug.LogWarning($"File '{relativePath}' not found.");
            }
        }
    }
}
#endif