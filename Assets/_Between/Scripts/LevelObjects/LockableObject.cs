using UnityEngine;

namespace Between.LevelObjects
{
    public abstract class LockableObject : InteractableObject
    {
        [Header("не трогать TipText")]
        [SerializeField] protected string _lockText;
        [SerializeField] protected string _unlockText;

        [SerializeField] private bool _isLocked = true;

        protected override void PerformOnInteract()
        {
            if (!_isLocked)
                InteractAfterUnlock();
        }

        protected abstract void InteractAfterUnlock();

        public void Lock()
        {
            _isLocked = true;
            TipText = _lockText;
        }

        public void Unlock()
        {
            _isLocked = false;
            TipText = _unlockText;
        }
    }
}