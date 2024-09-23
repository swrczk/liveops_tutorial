#if UNITY_EDITOR
using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    public class FileInProjectCriterion : Criterion
    {
        [SerializeField]
        private string filePath;


        public override void StartTesting()
        {
            base.StartTesting();
            UpdateCompletion();
            EditorApplication.update += UpdateCompletion;
        }
        
        public override void StopTesting()
        {
            base.StopTesting();
            EditorApplication.update -= UpdateCompletion;
        }
        
        protected override bool EvaluateCompletion()
        {
            Debug.Log("EvaluateCompletion called for FileInProjectCriterion");
            return IsFileInProject(filePath);
        } 

        public override bool AutoComplete()
        {
            return false;
        }

        private bool IsFileInProject(string relativePath)
        {
            var isFileInProject = AssetDatabase.LoadAssetAtPath<Object>(relativePath) != null;
            Debug.Log($"File {relativePath} is in project: {isFileInProject}");
            return isFileInProject;
        }

        private void OnProjectChanged()
        {
            Debug.Log("Project changed - rechecking file presence");
            UpdateCompletion();
        }
    }
}
#endif