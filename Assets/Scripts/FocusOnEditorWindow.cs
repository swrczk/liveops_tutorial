using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class FocusOnEditorWindow : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        public WindowTypeSelector windowTypeSelector;
        public void Focus()
        {
            FocusWindowIfItsOpen(windowTypeSelector.GetSelectedWindowType());
        }

        private static void FocusWindowIfItsOpen(System.Type windowType)
        {
            EditorWindow.FocusWindowIfItsOpen(windowType);
        }
#endif
    }
}