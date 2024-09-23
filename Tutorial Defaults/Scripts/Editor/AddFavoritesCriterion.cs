using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEditor;

namespace Editor
{
    public class AddFavoritesCriterion : Criterion
    {
        private List<string> _initialFavorites;
 

        public override void StartTesting()
        {
            base.StartTesting();
            _initialFavorites = GetFavorites();
            EditorApplication.update += UpdateCompletion;
        }
        // Odrejestrowanie eventu po zakończeniu testowania
        public override void StopTesting()
        {
            base.StopTesting();
            EditorApplication.update -= UpdateCompletion;
        }

        protected override bool EvaluateCompletion()
        {
            var currentFavorites = GetFavorites();
            return HasNewFavorite(currentFavorites);
        }

        private bool HasNewFavorite(List<string> currentFavorites)
        {
            foreach (var favorite in currentFavorites)
            {
                if (!_initialFavorites.Contains(favorite))
                {
                    return true;
                }
            }

            return false;
        }

        private List<string> GetFavorites()
        {
            var favoritePaths = new List<string>();
            var guids = AssetDatabase.FindAssets("l:favorite");
            foreach (var guid in guids)
            {
                favoritePaths.Add(AssetDatabase.GUIDToAssetPath(guid));
            }

            return favoritePaths;
        }

        public override bool AutoComplete()
        {
            return false;
        }
    }
}