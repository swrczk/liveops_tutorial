using System.Reflection;
using Unity.Tutorials.Core.Editor;
using UnityEditor;

namespace Editor
{
    public class AddFavoritesCriterion : Criterion
    {
        private string _searchText = string.Empty;


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
            _searchText = GetSearchText();
            ;
            return !string.IsNullOrEmpty(_searchText);
        }

        private string GetSearchText()
        {
            var projectBrowserType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ProjectBrowser");
            var projectBrowser = EditorWindow.GetWindow(projectBrowserType);

            if (projectBrowser != null)
            {
                var searchField = projectBrowserType.GetField("m_SearchFieldText",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                if (searchField != null)
                {
                    return (string) searchField.GetValue(projectBrowser);
                }
            }

            return string.Empty;
        }

        public override bool AutoComplete()
        {
            return false;
        }
    }
}