using UnityEngine;
using Between.SpellsEffects.Projectile;

namespace Between.ShieldsSpawning
{
    public partial class ProjectileTrigger
    {
        private class ProjectileTrajectoryData
        {
            public Projectile Projectile;
            public Vector3 EnterPoint;
            private readonly Transform _owner;
            public Vector3 ExitPoint;

            public ProjectileTrajectoryData(Projectile projectile, Vector3 enterPoint, Transform owner)
            {
                Projectile = projectile;
                EnterPoint = enterPoint;
                _owner = owner;
            }

            public void AddExitPoint(Vector3 point)
            {
                ExitPoint = point;
            }

            public bool CanHitTarget()
            {
                RaycastHit[] rayCastHits = Physics.SphereCastAll(ExitPoint, Projectile.SizeX / 2f, (ExitPoint - EnterPoint).normalized);

                foreach (var rayCastHit in rayCastHits)
                {
                    if (rayCastHit.transform == _owner)
                        return true;
                }

                return false;
            }
        }
    }
}