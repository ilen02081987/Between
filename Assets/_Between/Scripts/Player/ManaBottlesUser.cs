using System;
using UnityEngine;
using Between.Inventory;

namespace Between.MainCharacter
{
    public class ManaBottlesUser : MonoBehaviour
    {
        public event Action BottleUsed;

        [SerializeField] private KeyCode _key;

        private ManaBottlesHolder _holder;

        public void Init(ManaBottlesHolder holder)
        {
            _holder = holder;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_key) && _holder.Count > 0)
            {
                _holder.Apply();
                BottleUsed?.Invoke();
            }
        }
    }
}