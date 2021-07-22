using System.IO;
using UnityEngine;
using Between.Collisions;
using Between.Utilities;
using System.Collections;

namespace Between.SpellsEffects.Projectile
{
    public class ProjectileSpawner
    {
        public float ElementSize => _prefab.transform.localScale.z;

        private Projectile _prefab;
        private static GameObject _projectilesParent;

        private readonly float _offset;
        private readonly float _spawnDelay;

        public ProjectileSpawner(string prefabName, float spawnOffset, float spawnDelay = 0f)
        {
            _prefab = Resources.Load<Projectile>(Path.Combine(ResourcesFoldersNames.SPELLS, prefabName));

            if (_projectilesParent == null)
                _projectilesParent = new GameObject("ProjectilesParent");

            _offset = spawnOffset;
            _spawnDelay = spawnDelay;
        }

        public void Spawn(Vector3 position, Vector3 direction)
        {
            if (!SpaceDetector.IsFreeSpace(position, ElementSize / 2f))
                return;

            var spawnPosition = FindSpawnPoint(position, direction);
            var projectile = MonoBehaviour.Instantiate(
                _prefab, spawnPosition, Quaternion.identity, _projectilesParent.transform);

            CoroutineLauncher.Start(DelayedLaunch(projectile, direction));
        }

        private Vector3 FindSpawnPoint(Vector3 position, Vector3 direction)
        {
            return position - direction * _offset;
        }

        private IEnumerator DelayedLaunch(Projectile projectile, Vector3 direction)
        {
            yield return new WaitForSeconds(_spawnDelay);
            projectile.Launch(direction);
        }
    }
}