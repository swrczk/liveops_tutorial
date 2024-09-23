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
        private FutureObjectReference futureObjectReference;

        private string _initialSpriteName;
        private GameObject _target;


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
            if (futureObjectReference.SceneObjectReference.ReferencedObject == null)
                return false;
            
            if (_target != null)
            {
                var imageComponent = _target.GetComponent<Image>();
                if (imageComponent != null)
                    return imageComponent.sprite.name != _initialSpriteName;
            }
            else
            {
                _target = futureObjectReference.SceneObjectReference.ReferencedObject.GameObject();
                var imageComponent = _target.GetComponent<Image>();
                _initialSpriteName = imageComponent.sprite.name;
            }

            return false;
        }

        public override bool AutoComplete()
        {
            return false;
        }
    }
}