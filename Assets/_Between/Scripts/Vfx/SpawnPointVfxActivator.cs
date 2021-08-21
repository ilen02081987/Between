using Between.Spawning;
using UnityEngine;

namespace Between.Vfx
{
    public class SpawnPointVfxActivator : MonoBehaviour
    {
        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private GameObject _vfxPrefab;

        private void Start()
        {
            _spawnPoint.OnSpawn += ActivateVfx;
        }

        private void ActivateVfx()
        {
            _spawnPoint.OnSpawn -= ActivateVfx;
            Instantiate(_vfxPrefab, transform.position, Quaternion.identity);
        }
    }
}