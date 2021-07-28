using UnityEngine;
using Between.SpellsEffects.Projectile;

namespace Between.ShieldsSpawning
{
    public partial class ProjectileTrigger
    {
        private class ProjectileTragectoryData
        {
            public Projectile Projectile;
            public Vector3 EnterPoint;
            private readonly Transform _target;
            public Vector3 ExitPoint;

            public ProjectileTragectoryData(Projectile projectile, Vector3 enterPoint, Transform owner)
            {
                Projectile = projectile;
                EnterPoint = enterPoint;
                _target = owner;
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
                    if (rayCastHit.transform == _target)
                        return true;
                }

                return false;
            }
        }
    }
}