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
            var target = FindObjectOfType<UiElementVisibleCriterionTarget>();

            if (target == null)
            {
                return false;
            }

            Debug.Log("UiElementVisibleCriterionTarget found in the scene");

            var uiElement = target.GetComponent<RectTransform>();
            Canvas canvas = uiElement.GetComponentInParent<Canvas>();
            if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                return uiElement.gameObject.activeInHierarchy;
            }

            return false;
        }

        public override bool AutoComplete()
        {
            return false;
        }
    }
}