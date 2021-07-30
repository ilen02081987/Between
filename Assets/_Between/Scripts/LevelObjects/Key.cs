using UnityEngine;

namespace Between.LevelObjects
{
    public class Key : InteractableObject
    {
        [SerializeField] private LockableObject _lockableObject;
        public override void Interact()
        {
            _lockableObject.Unlock();
            Destroy();
        }

        protected override void DestroyOnLoad() => Interact();
    }
}