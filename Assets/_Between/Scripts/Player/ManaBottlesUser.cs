using Between.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.MainCharacter
{
    public class ManaBottlesUser : MonoBehaviour
    {
        [SerializeField] private KeyCode _key;

        private ManaBottlesHolder _holder;

        public void Init(ManaBottlesHolder holder)
        {
            _holder = holder;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_key) && _holder.Count > 0)
                _holder.Apply();
        }
    }
}