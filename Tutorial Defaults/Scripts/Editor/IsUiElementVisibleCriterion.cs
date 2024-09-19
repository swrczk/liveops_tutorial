using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class IsUiElementVisibleCriterion : Criterion
    {
        [SerializeField]
        private GameObject prefabToCheck;


        private void OnEnable()
        {
            EditorApplication.update += UpdateCompletion;
        }

        private void OnDisable()
        {
            EditorApplication.update -= UpdateCompletion;
        }

        protected override bool EvaluateCompletion()
        {
            var targets = FindObjectsOfType<UiElementVisibleCriterionTarget>();

            foreach (var target in targets)
            {
                if (target.gameObject.name != prefabToCheck.name)
                {
                    continue;
                }

                Debug.Log("UiElementVisibleCriterionTarget found in the scene");

                var uiElement = target.GetComponent<RectTransform>();
                var canvas = uiElement.GetComponentInParent<Canvas>();
                if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                {
                    return uiElement.gameObject.activeInHierarchy && uiElement.gameObject.activeSelf;
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