using System;
using UnityEngine;

namespace Between.LevelObjects
{
    [RequireComponent(typeof(Collider))]
    public class FightBorder : MonoBehaviour
    {
        public event Action OnPlayerExitCollider;

        private Collider _collider;

        private void Start()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
                OnPlayerExitCollider?.Invoke();
        }

        public void Enable() => _collider.isTrigger = false;
        public void Destroy() => Destroy(gameObject);
    }
}