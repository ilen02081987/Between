using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between;
using Between.Enemies.Mavka;

public partial class ProjectileTrigger
{
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
            if (Physics.SphereCast(ExitPoint, GameSettings.Instance.SeveralProjectilesSphereCastRadius,
                (ExitPoint - EnterPoint).normalized, out RaycastHit info, 100f))
                return info.collider.TryGetComponent<MavkaController>(out var mavka);

            return false;
        }
    }
}
