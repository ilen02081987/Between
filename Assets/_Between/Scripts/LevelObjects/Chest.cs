using System;
using UnityEngine;

namespace Between.LevelObjects
{
    public class Chest : LockableObject
    {
        public event Action OnOpen;

        [SerializeField] private GameObject[] _loots;

        private void Start()
        {
            TipText = _lockText;
        }

        protected override void InteractAfterUnlock()
        {
            OnOpen?.Invoke();

            foreach (GameObject loot in _loots)
                loot?.SetActive(true);

            Lock();
        }
    }
}