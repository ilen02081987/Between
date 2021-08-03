using System.IO;
using UnityEngine;
using Between.Collisions;
using Between.Utilities;
using System.Collections;

namespace Between.SpellsEffects.Projectile
{
    public class ProjectileSpawner
    {
        public float ElementSize => _prefab.SizeZ;

        private Projectile _prefab;
        private static GameObject _projectilesParent;

        public ProjectileSpawner(string prefabName)
        {
            _prefab = Resources.Load<Projectile>(Path.Combine(ResourcesFoldersNames.SPELLS, prefabName));

            if (_projectilesParent == null)
                _projectilesParent = new GameObject("ProjectilesParent");
        }

        public void Spawn(Vector3 position, Vector3 direction)
        {
            if (!SpaceDetector.IsFreeSpace(position, ElementSize / 2f))
                return;

            Vector3 spawnPosition = position - direction;
            var projectile = MonoBehaviour.Instantiate(
                _prefab, spawnPosition, Quaternion.identity, _projectilesParent.transform);

            projectile.Launch(direction);
        }

        public void ChangeDamageValue(float to) => _prefab.ChangeDamageValue(to);
    }
}