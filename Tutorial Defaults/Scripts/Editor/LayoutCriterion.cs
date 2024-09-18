using System;
using System.IO;
using UnityEditor;
using Unity.Tutorials.Core.Editor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    public class LayoutCriterion : Criterion
    {
        [SerializeField]
        private string layoutFileName = "tutorial";

        private void OnEnable()
        {
            EditorApplication.projectChanged += OnProjectChanged;
        }

        public override void StartTesting()
        {
            base.StartTesting();
            Debug.Log("StartTesting called for LayoutCriterion");
            UpdateCompletion();
        }

        protected override bool EvaluateCompletion()
        {
            Debug.Log("EvaluateCompletion called for LayoutCriterion");
            return IsLayoutLoaded(layoutFileName);
        }

        public override bool AutoComplete()
        {
            return true;
        }

        private bool IsLayoutLoaded(string layoutPath)
        {
            // Sprawdzamy, czy plik layoutu istnieje
            if (AssetDatabase.LoadAssetAtPath<Object>(layoutPath) == null)
            {
                Debug.Log($"Layout file does not exist: {layoutPath}");
                return false;
            }

            // Ścieżka do bieżącego załadowanego layoutu (ta informacja nie jest bezpośrednio dostępna w Unity)
            // Możemy jednak porównać pliki w folderze z layoutami i zobaczyć, który był ostatnio modyfikowany.
            var currentLayoutPath = GetCurrentLayoutFilePath();

            // Sprawdzamy, czy ścieżka do layoutu odpowiada aktualnemu plikowi layoutu
            if (string.Equals(layoutPath, currentLayoutPath, System.StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"The current layout matches the desired layout: {layoutPath}");
                return true;
            }

            Debug.Log(
                $"The current layout does not match the desired layout. Expected: {layoutPath}, Current: {currentLayoutPath}");
            return false;
        }

        private void OnProjectChanged()
        {
            // Sprawdzaj ponownie kryterium po zmianie w projekcie
            Debug.Log("Project changed - rechecking layout");
            UpdateCompletion();
        }

        private string GetCurrentLayoutFilePath()
        {
            // Pobieramy folder, w którym Unity zapisuje layouty
            var layoutsFolder = Path.Combine(InternalEditorUtility.unityPreferencesFolder, "Layouts");

            // Sprawdzamy wszystkie pliki w folderze Layouts
            var layoutFiles = Directory.GetFiles("Assets/Layouts", "*.wlt");

            // Znajdujemy plik, który był ostatnio modyfikowany
            string mostRecentFile = null;
            var mostRecentTime = DateTime.MinValue;

            foreach (var layoutFile in layoutFiles)
            {
                var lastWriteTime = File.GetLastAccessTime(layoutFile);
                if (lastWriteTime > mostRecentTime)
                {
                    mostRecentFile = layoutFile;
                    mostRecentTime = lastWriteTime;
                }
            }

            Debug.Log($"Most recently modified layout file: {NormalizePath(mostRecentFile)}");
            return NormalizePath(mostRecentFile);
        }

        private string NormalizePath(string path)
        {
            return path.Replace("\\", "/");
        }
    }
}