using UnityEngine;
using System;
using Unity.Tutorials.Core.Editor;
using UnityEditor;

namespace Editor
{
#if UNITY_EDITOR
    public class ResolutionCriterion : Criterion
    {
        [SerializeField]
        private int width = 1080;

        [SerializeField]
        private int height = 1920 ; 

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
            return Screen.width == width && Screen.height == height;
        } 
    }
#endif
}