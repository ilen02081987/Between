using UnityEngine;

namespace Between.Spawning
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnedObject;
        [SerializeField] private Transform _parent;
        [SerializeField] private GameObject _vfx;

        public GameObject Spawn()
        {
            TryEnableVfx();
            return Instantiate(_spawnedObject, transform.position, Quaternion.identity, _parent);
        }

        private void TryEnableVfx()
        {
            if (_vfx != null)
                Instantiate(_vfx, transform.position, Quaternion.identity);
        }
    }
}