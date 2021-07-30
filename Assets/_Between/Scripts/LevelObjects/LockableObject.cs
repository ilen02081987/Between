using UnityEngine;

namespace Between.LevelObjects
{
    public abstract class LockableObject : InteractableObject
    {
        [SerializeField] private bool _isLocked;

        public override void Interact()
        {
            if (!_isLocked)
                InteractAfterUnlock();
        }

        protected abstract void InteractAfterUnlock();

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }
    }
}