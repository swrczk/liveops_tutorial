using UnityEngine;
using System;
using Unity.Tutorials.Core.Editor;
using UnityEditor;

namespace Editor
{
#if UNITY_EDITOR
    public class AspectRatioCriterion : Criterion
    {
        [SerializeField]
        private int aspectWidth = 9;

        [SerializeField]
        private int aspectHeight = 16; 

        public void OnEnable()
        {
            EditorApplication.update += UpdateCompletion;
        }

        public void OnDisable()
        {
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
            var aspectRatio = (float) Screen.width / Screen.height;
            var expectedAspectRatio = (float) aspectWidth / aspectHeight;
            return Math.Abs(aspectRatio - expectedAspectRatio) < 0.01f;
        } 
    }
#endif
}