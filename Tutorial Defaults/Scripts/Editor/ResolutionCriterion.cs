using UnityEngine;
using System;
using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Editor
{
#if UNITY_EDITOR
    public class ResolutionCriterion : Criterion
    {
        [SerializeField]
        private int width = 1080;

        [SerializeField]
        private int height = 1920;

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

        public override bool AutoComplete()
        {
            return false;
        }

        protected override bool EvaluateCompletion()
        {
            return IsAspectRatioApplied();
        }


        private bool IsAspectRatioApplied()
        {
            return Screen.width == width && Screen.height == height;
        }
    }
#endif
}