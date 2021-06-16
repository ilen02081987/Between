using Between.SpellsEffects.Projectile;
using Between.UserInput.Trackers;
using UnityEngine;

namespace Between.Spells
{
    public class ProjectileSpell : BaseSpell
    {
        public override float CoolDownTime => 1f;

        protected override BaseInputTracker tracker => _tracker;
        private readonly CurveTracker _tracker = new CurveTracker(0).SetForceEndAngle(50f).SetLenght(20, 1000);

        private readonly ProjectileSpawner _projectileSpawner;
        private readonly float _spawnOffset = 2f;

        public ProjectileSpell(string projectileName)
        {
            _projectileSpawner = new ProjectileSpawner(projectileName, _spawnOffset);
        }

        protected override void OnCompleteSpell()
        {
            var points = _tracker.DrawPoints;
            var startPoint = ConvertVector(points[0]);
            var directionPoint = ConvertVector(points[points.Count - 1]);

            _projectileSpawner.Spawn(startPoint, (directionPoint - startPoint).normalized);
        }

        private Vector3 ConvertVector(Vector3 input) => GameCamera.ScreenToWorldPoint(input);
    }
}