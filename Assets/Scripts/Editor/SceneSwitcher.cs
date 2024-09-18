#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    public class SceneSwitcher : MonoBehaviour
    {
        public static void LoadSceneByName(string sceneName)
        {
            EditorSceneManager.OpenScene($"Assets/Scenes/{sceneName}.unity");
        }
    }
}
#endif