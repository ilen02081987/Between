using System.IO;
using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    public class ProjectileSpawner
    {
        public float ElementSize => _prefab.transform.localScale.z;

        private Projectile _prefab;
        private readonly GameObject _projectilesParent;

        private readonly float _offset;

        public ProjectileSpawner(string prefabName, float spawnOffset)
        {
            _prefab = Resources.Load<Projectile>(Path.Combine(ResourcesFoldersNames.SPELLS, prefabName));
            _projectilesParent = new GameObject("ProjectilesParent");

            _offset = spawnOffset;
        }

        public void Spawn(Vector3 position, Vector3 direction)
        {
            var spawnPosition = FindSpawnPoint(position, direction);
            var projectile = MonoBehaviour.Instantiate(
                _prefab, spawnPosition, Quaternion.identity, _projectilesParent.transform);

            projectile.Launch(direction);
        }

        private Vector3 FindSpawnPoint(Vector3 position, Vector3 direction)
        {
            return position - direction * _offset;
        }
    }
}