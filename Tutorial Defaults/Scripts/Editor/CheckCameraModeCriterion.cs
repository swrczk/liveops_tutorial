using System;
using Unity.Tutorials.Core.Editor;
using UnityEditor;

namespace Editor
{
    using UnityEngine;

    public class CheckCameraModeCriterion : Criterion
    {
        [SerializeField]
        private bool shouldBe2D;

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
            var sceneView = SceneView.lastActiveSceneView;
            if (sceneView == null)
                return false;

            return (shouldBe2D ? sceneView.in2DMode : !sceneView.in2DMode);
        }

        public override bool AutoComplete()
        {
            return false;
        }
    }
}