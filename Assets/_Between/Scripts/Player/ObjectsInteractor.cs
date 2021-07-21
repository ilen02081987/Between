using System;
using UnityEngine;
using Between.LevelObjects;

namespace Between.MainCharacter
{
    public class ObjectsInteractor : MonoBehaviour
    {
        public event Action<InteractableObject> OnObjectEnter;
        public event Action OnObjectExit;

        private InteractableObject _collidedObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<InteractableObject>(out var interactable))
            {
                OnObjectEnter?.Invoke(interactable);
                _collidedObject = interactable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<InteractableObject>(out var interactable))
            {
                _collidedObject = null;
                OnObjectExit?.Invoke();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _collidedObject != null)
            {
                _collidedObject.Interact();

                if (_collidedObject.IsDestroyed)
                    OnObjectExit?.Invoke();
            }
        }
    }
}