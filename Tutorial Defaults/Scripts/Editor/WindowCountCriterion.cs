using System.Reflection;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using UnityEditor;

namespace Editor
{
    public class WindowCountCriterion : Criterion
    {
        [SerializeField]
        private WindowTypeSelector windowTypeSelector;

        [SerializeField]
        private int windowCount;

        [SerializeField]
        private bool isAnyLocked;


        public override void StartTesting()
        {
            base.StartTesting();
            EditorApplication.update += UpdateCompletion;
        }

        public override void StopTesting()
        {
            base.StopTesting();
            EditorApplication.update -= UpdateCompletion;
        }

        protected override bool EvaluateCompletion()
        {
            var inspectorWindowType = windowTypeSelector.GetSelectedWindowType();
            if (inspectorWindowType == null)
            {
                Debug.LogError("WindowCountCriterion - Nie można znaleźć typu okna.");
                return false;
            }


            var inspectorWindows = Resources.FindObjectsOfTypeAll(inspectorWindowType);
            if (!isAnyLocked)
            {
                return inspectorWindows.Length >= windowCount;
            }

            if (inspectorWindows.Length < windowCount)
            {
                return false;
            }

            var isLockedField = inspectorWindowType.GetProperty("isLocked",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (isLockedField == null)
            {
                Debug.LogError("Nie można znaleźć pola m_IsLocked.");
                return false;
            }

            foreach (var inspectorWindow in inspectorWindows)
            {
                bool isLocked = (bool) isLockedField.GetValue(inspectorWindow);

                if (isLocked)
                {
                    return true;
                }
            }

            return false;
        }


        public override bool AutoComplete()
        {
            return false;
        }
    }
}