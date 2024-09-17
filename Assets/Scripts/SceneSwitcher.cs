#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    // Metoda do ładowania sceny po nazwie
    public static void LoadSceneByName(string sceneName)
    {
        EditorSceneManager.OpenScene($"Assets/Scenes/{sceneName}.unity");
        // Assets/Scenes/StartScene.unity
    }
}
#endif