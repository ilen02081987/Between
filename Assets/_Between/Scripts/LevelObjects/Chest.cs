using System;
using UnityEngine;

namespace Between.LevelObjects
{
    public class Chest : LockableObject
    {
        public event Action OnOpen;

        [SerializeField] private GameObject[] _loots;

        protected override void InteractAfterUnlock()
        {
            OnOpen?.Invoke();

            foreach (GameObject loot in _loots)
                if (loot != null)
                    loot.SetActive(true);

            Clear();
        }
    }
}