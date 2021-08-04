using UnityEngine;

namespace Between.LevelObjects
{
    public class Key : InteractableObject
    {
        [SerializeField] private LockableObject _lockableObject;
        protected override void PerformOnInteract()
        {
            _lockableObject.Unlock();
            Destroy();
        }

        public override void DestroyOnLoad() => Interact();
    }
}