using System.IO;
using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    public class ProjectileSpawner
    {
        private Projectile _prefab;
        private GameObject _projectilesParent;

        private ProjectileData _projectileData;

        private float _offset = 2f;

        public ProjectileSpawner(ProjectileData projectileData)
        {
            _projectileData = projectileData;

            _prefab = Resources.Load<Projectile>(Path.Combine(ResourcesFoldersNames.SPELLS, "Projectile"));
            _projectilesParent = new GameObject("ProjectilesParent");
        }

        public void Spawn(Vector3 position, Vector3 direction)
        {
            var spawnPosition = FindSpawnPoint(position, direction);
            var projectile = MonoBehaviour.Instantiate(
                _prefab, spawnPosition, Quaternion.identity, _projectilesParent.transform);

            projectile.Launch(_projectileData, direction);
        }

        private Vector3 FindSpawnPoint(Vector3 position, Vector3 direction)
        {
            return position - direction * _offset;
        }
    }
}