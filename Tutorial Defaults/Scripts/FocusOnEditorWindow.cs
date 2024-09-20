using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class FocusOnEditorWindow : MonoBehaviour
    {
#if UNITY_EDITOR
        public static void Focus([SerializeField]WindowTypeSelector windowTypeSelector)
        {
            EditorWindow.FocusWindowIfItsOpen(windowTypeSelector.GetSelectedWindowType());
        }
#endif
    }
}