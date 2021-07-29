using System;
using System.Collections.Generic;
using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.Teams;

namespace Between.ShieldsSpawning
{
    public partial class ProjectileTrigger : MonoBehaviour
    {
        public event Action OnDetect;

        [SerializeField] private Transform _owner;

        private List<ProjectileTrajectoryData> _projectileTragectoryDatas = new List<ProjectileTrajectoryData>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Projectile>(out var projectile))
                if (projectile.Team != Team.Enemies)
                    _projectileTragectoryDatas.Add(
                        new ProjectileTrajectoryData(projectile, other.transform.position, _owner));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Projectile>(out var projectile))
            {
                var projectileData = FindData(projectile);

                if (projectileData == null)
                    return;

                projectileData.AddExitPoint(other.transform.position);

                if (projectileData.CanHitTarget())
                    OnDetect?.Invoke();

                _projectileTragectoryDatas.Remove(projectileData);
            }
        }

        private ProjectileTrajectoryData FindData(Projectile projectile)
            => _projectileTragectoryDatas.Find(x => x.Projectile == projectile);
    }
}