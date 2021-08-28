using UnityEngine;

namespace Between.LevelObjects
{
    public class Key : InteractableObject
    {
        [SerializeField] private LockableObject _lockableObject;
        [SerializeField] private GameObject _keyBoss;

        protected override void PerformOnInteract()
        {
            _lockableObject.Unlock();

            if (_keyBoss != null)
                _keyBoss.SetActive(true);

            Destroy();
        }

        public override void DestroyOnLoad() => Interact();
    }
}