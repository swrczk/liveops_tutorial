using Unity.Tutorials.Core.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    public class ImageSpriteChangedCriterion : Criterion
    {
        [SerializeField]
        private FutureObjectReference futureObjectReference; // Referencja do obiektu śledzonego (Future Object)

        private Sprite initialSprite;
        private GameObject target;

        public void OnDisable()
        {
            EditorApplication.update -= UpdateCompletion;
        }

        private void OnEnable()
        {
            EditorApplication.update += UpdateCompletion;  
        }
        public override void StartTesting()
        {
            base.StartTesting();
             target = futureObjectReference.SceneObjectReference.ReferencedObject.GameObject(); 
            
        }


        protected override bool EvaluateCompletion()
        {
            // Sprawdź, czy obiekt referencji istnieje i ma komponent Image
            if (futureObjectReference != null)
            {
                if (target != null)
                {
                    Image imageComponent = futureObjectReference.SceneObjectReference.ReferencedObjectAsGameObject.gameObject
                        ?.GetComponent<Image>();
                    Debug.Log("ImageSpriteChangedCriterion: EvaluateCompletion called");
                    if (imageComponent != null)
                    {
                        // Sprawdź, czy sprite zmienił się względem początkowego
                        Debug.Log("ImageSpriteChangedCriterion: Image component found");
                    }
                    else

                        return imageComponent.sprite != initialSprite;
                }
                else
                {
                    

                    Debug.Log("ImageSpriteChangedCriterion: Object reference is null");
                    Image imageComponent = target.GetComponent<Image>();
                    if (imageComponent != null)
                    {
                        // Zapamiętaj początkowy sprite
                        initialSprite = imageComponent.sprite;
                    }
                }
            }
            else
                Debug.Log("ImageSpriteChangedCriterion: Image component not found");

            return false; // Zwraca false, jeśli nie znaleziono komponentu Image lub sprite się nie zmienił
        }

        public override bool AutoComplete()
        {
            // Możesz dodać opcję automatycznego zmieniania sprite'a, jeśli potrzebne
            return false;
        }
    }
}