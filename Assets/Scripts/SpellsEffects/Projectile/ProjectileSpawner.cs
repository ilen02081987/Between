using Between.Teams;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Between.SpellsEffects.Projectile
{
    public class ProjectileSpawner
    {
        private Projectile _prefab;
        private GameObject _projectilesParent;
        private Team _ownerTeam;

        public ProjectileSpawner(Team ownerTeam)
        {
            _ownerTeam = ownerTeam;

            _prefab = Resources.Load<Projectile>(Path.Combine(ResourcesFoldersNames.SPELLS, "Projectile"));
            _projectilesParent = new GameObject("ProjectilesParent");
        }

        public void Spawn(Vector3 position, Vector3 direction, float speed, float damage)
        {
            var projectile = MonoBehaviour.Instantiate(_prefab, position, Quaternion.identity, _projectilesParent.transform);
            projectile.Launch(direction * speed, damage, _ownerTeam);
        }
    }
}