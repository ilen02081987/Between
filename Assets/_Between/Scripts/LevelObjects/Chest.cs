using UnityEngine;

namespace Between.LevelObjects
{
    public partial class Chest : LockableObject
    {
        [SerializeField] private GameObject[] _loots;

        private void Start()
        {
            TipText = _lockText;
        }

        protected override void InteractAfterUnlock()
        {
            foreach (GameObject loot in _loots)
                loot.SetActive(true);

            Lock();
        }
    }
}