using UnityEngine;

namespace Between.LevelObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public string TipText;
        public bool IsDestroyed { get; private set; } = false;

        public abstract void Interact();

        protected void Destroy()
        {
            Destroy(gameObject);
            IsDestroyed = true;
        }
    }
}