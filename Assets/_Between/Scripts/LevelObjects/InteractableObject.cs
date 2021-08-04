using System;
using UnityEngine;

namespace Between.LevelObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public event Action OnInteract;

        public string Name;
        public string TipText;
        public bool IsDestroyed { get; private set; } = false;

        public void Interact()
        {
            PerformOnInteract();
            OnInteract?.Invoke();
        }

        protected abstract void PerformOnInteract();

        protected void Destroy()
        {
            Destroy(gameObject);
            IsDestroyed = true;
        }

        public virtual void DestroyOnLoad() { }
    }
}