using System.Collections.Generic;
using UnityEngine;
using Between.Enemies.Mavka;
using Between.SpellsEffects.Projectile;
using Between.Teams;

public class ProjectileTrigger : MonoBehaviour
{
    private List<ProjectileTragectoryData> _projectileTragectoryDatas = new List<ProjectileTragectoryData>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out var projectile))
            if (projectile.Team != Team.Enemies)
                _projectileTragectoryDatas.Add(new ProjectileTragectoryData(projectile, other.transform.position));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out var projectile))
        {
            var projectileData = FindData(projectile);

            if (projectileData != null)
            {
                projectileData.AddExitPoint(other.transform.position);

                if (projectileData.CanHitMavka())
                    TrySpawnShield();
            }
        }
    }

    private static void TrySpawnShield()
    {

    }

    private ProjectileTragectoryData FindData(Projectile projectile) 
        => _projectileTragectoryDatas.Find(x => x.Projectile == projectile);
    private bool HasProjectileData(Projectile projectile) => FindData(projectile) != null;

    private class ProjectileTragectoryData
    {
        public Projectile Projectile;
        public Vector3 EnterPoint;
        public Vector3 ExitPoint;

        public ProjectileTragectoryData(Projectile projectile, Vector3 enterPoint)
        {
            Projectile = projectile;
            EnterPoint = enterPoint;
        }

        public void AddExitPoint(Vector3 point)
        {
            ExitPoint = point;
        }

        public bool CanHitMavka()
        {
            if (Physics.Raycast(ExitPoint, (ExitPoint - EnterPoint).normalized, out RaycastHit info, 100f))
                return info.collider.TryGetComponent<MavkaController>(out var mavka);

            return false;
        }
    }
}
